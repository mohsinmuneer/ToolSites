using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace ToolSite.Core;

// Sirf POST convert actions par lagao ([ServiceFilter(typeof(DailyQuotaFilter))]), page views par nahi.
// Quota sirf kamyab conversion par ghatta hai. Production mein MemoryCache ki jagah Redis/SQL lo.
public class DailyQuotaFilter : IAsyncActionFilter
{
    private readonly IMemoryCache _cache;
    private readonly LimitsOptions _limits;
    public DailyQuotaFilter(IMemoryCache cache, IOptions<LimitsOptions> limits)
    { _cache = cache; _limits = limits.Value; }

    public async Task OnActionExecutionAsync(ActionExecutingContext ctx, ActionExecutionDelegate next)
    {
        var http = ctx.HttpContext;
        var ip = http.Connection.RemoteIpAddress?.ToString() ?? "unknown";
        var vid = http.Request.Cookies["vid"];
        if (string.IsNullOrEmpty(vid))
        {
            vid = Guid.NewGuid().ToString("N");
            http.Response.Cookies.Append("vid", vid, new CookieOptions
            { HttpOnly = true, Secure = http.Request.IsHttps, SameSite = SameSiteMode.Lax, Expires = DateTimeOffset.UtcNow.AddDays(30) });
        }
        var day = DateTime.UtcNow.ToString("yyyyMMdd");
        var vKey = $"q:{day}:v:{vid}";
        var iKey = $"q:{day}:ip:{ip}";
        var tomorrow = DateTime.UtcNow.Date.AddDays(1);

        // Cookie hata kar bypass rokne ke liye IP ki limit 4 guna rakhi hai (office/mobile network ke liye).
        if (_cache.Get<int>(vKey) >= _limits.DailyFreeQuota || _cache.Get<int>(iKey) >= _limits.DailyFreeQuota * 4)
        {
            ctx.Result = new ObjectResult(new { error = $"Daily free limit ({_limits.DailyFreeQuota}) reached. Come back tomorrow or upgrade.", upgrade = true })
            { StatusCode = StatusCodes.Status429TooManyRequests };
            return;
        }
        var executed = await next();
        if (executed.Result is FileResult)
        {
            _cache.Set(vKey, _cache.Get<int>(vKey) + 1, tomorrow);
            _cache.Set(iKey, _cache.Get<int>(iKey) + 1, tomorrow);
        }
    }
}

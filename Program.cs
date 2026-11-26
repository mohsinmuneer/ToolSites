using System.Threading.RateLimiting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;
using ToolSite.Services;

var builder = WebApplication.CreateBuilder(args);
// On Render, use the public URL unless a custom canonical origin is configured.
if (Uri.TryCreate(builder.Configuration["RENDER_EXTERNAL_URL"], UriKind.Absolute, out var renderUrl)
    && renderUrl.Scheme == Uri.UriSchemeHttps
    && Uri.TryCreate(builder.Configuration["Seo:BaseUrl"], UriKind.Absolute, out var configuredUrl)
    && configuredUrl.IsLoopback)
{
    builder.Configuration["Seo:BaseUrl"] = renderUrl.GetLeftPart(UriPartial.Authority);
}
ToolRegistry.Hidden = new HashSet<string>(builder.Configuration.GetSection("Tools:Hidden").Get<string[]>() ?? Array.Empty<string>());

// Preview all registered tools locally while preserving the production publication list.
if (builder.Environment.IsDevelopment() && builder.Configuration.GetValue<bool>("Tools:PreviewAll"))
    ToolRegistry.Hidden.Clear();

builder.Services.AddControllersWithViews(o => o.Filters.Add(new AutoValidateAntiforgeryTokenAttribute()));
builder.Services.AddMemoryCache();                        // production: Redis lo
builder.Services.Configure<LimitsOptions>(builder.Configuration.GetSection("Limits"));
builder.Services.AddScoped<DailyQuotaFilter>();
builder.Services.AddScoped<IJpgToPdfService, JpgToPdfService>();
builder.Services.AddHostedService<TempFileCleanupService>();

const long MaxBody = 60L * 1024 * 1024;                   // 10 files x ~6MB ka andaza
builder.WebHost.ConfigureKestrel(o => o.Limits.MaxRequestBodySize = MaxBody);
builder.Services.Configure<FormOptions>(o => o.MultipartBodyLengthLimit = MaxBody);

// Cloudflare/Nginx ke peeche asli client IP. Sirf tab theek jab server seedha internet par na ho.
builder.Services.Configure<ForwardedHeadersOptions>(o =>
{
    o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
    o.KnownNetworks.Clear(); o.KnownProxies.Clear();
});

builder.Services.AddRateLimiter(o =>
{
    o.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    o.AddPolicy("burst", ctx => RateLimitPartition.GetFixedWindowLimiter(
        ctx.Connection.RemoteIpAddress?.ToString() ?? "anon",
        _ => new FixedWindowRateLimiterOptions { PermitLimit = 10, Window = TimeSpan.FromMinutes(1) }));
});

var app = builder.Build();
app.UseForwardedHeaders();
if (!app.Environment.IsDevelopment()) { app.UseExceptionHandler("/error"); app.UseHsts(); }
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseRateLimiter();
app.Use(async (ctx, next) =>
{
    ctx.Response.Headers["X-Content-Type-Options"] = "nosniff";
    ctx.Response.Headers["X-Frame-Options"] = "DENY";
    ctx.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    await next();
});
app.MapControllers();
app.MapGet("/healthz", () => Results.Text("ok", "text/plain"));
app.Run();

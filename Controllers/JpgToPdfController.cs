using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using ToolSite.Core;
using ToolSite.Services;

namespace ToolSite.Controllers;

[Route("jpg-to-pdf")]
public class JpgToPdfController : ToolBaseController
{
    private readonly IJpgToPdfService _svc;
    public JpgToPdfController(IJpgToPdfService svc) => _svc = svc;

    [HttpGet("")]
    public IActionResult Index() => ToolView("jpg-to-pdf");

    [HttpPost("convert")]
    [ServiceFilter(typeof(DailyQuotaFilter))]
    [EnableRateLimiting("burst")]
    public async Task<IActionResult> ConvertToPdf(List<IFormFile> files, string pageSize = "A4")
    {
        var error = await ValidateImagesAsync(files);
        if (error != null) return BadRequest(new { error });

        var list = new List<byte[]>();
        foreach (var f in files)
        {
            using var ms = new MemoryStream();
            await f.CopyToAsync(ms);
            list.Add(ms.ToArray());
        }
        try
        {
            var pdf = _svc.Convert(list, pageSize);
            return File(pdf, "application/pdf", "images.pdf");
        }
        catch
        {
            return BadRequest(new { error = "We could not read one of the images. Try a different file." });
        }
    }
}

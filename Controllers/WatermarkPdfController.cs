using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("watermark-pdf")]
public class WatermarkPdfController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("watermark-pdf");
}

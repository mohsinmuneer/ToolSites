using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("pdf-to-powerpoint")]
public class PdfToPowerPointController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("pdf-to-powerpoint");
}

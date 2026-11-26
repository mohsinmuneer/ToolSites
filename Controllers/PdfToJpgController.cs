using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("pdf-to-jpg")]
public class PdfToJpgController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("pdf-to-jpg");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("rotate-pdf")]
public class RotatePdfController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("rotate-pdf");
}

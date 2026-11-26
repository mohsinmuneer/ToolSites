using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("split-pdf")]
public class SplitPdfController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("split-pdf");
}

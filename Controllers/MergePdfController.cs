using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("merge-pdf")]
public class MergePdfController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("merge-pdf");
}

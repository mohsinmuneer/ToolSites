using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("compress-pdf")]
public class CompressPdfController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("compress-pdf");
}

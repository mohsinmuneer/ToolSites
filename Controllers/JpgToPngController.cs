using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("jpg-to-png")]
public class JpgToPngController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("jpg-to-png");
}

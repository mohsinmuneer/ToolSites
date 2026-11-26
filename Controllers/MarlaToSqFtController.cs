using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("marla-to-sq-ft")]
public class MarlaToSqFtController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("marla-to-sq-ft");
}

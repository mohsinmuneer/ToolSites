using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("inches-to-cm")]
public class InchesToCmController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("inches-to-cm");
}

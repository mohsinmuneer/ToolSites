using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("tip-calculator")]
public class TipCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("tip-calculator");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

// Client-side tool: hisaab browser mein hota hai, server par kuch upload nahi hota.
[Route("time-calculator")]
public class TimeCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolView("time-calculator");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

// Client-side tool: hisaab browser mein hota hai, server par kuch upload nahi hota.
[Route("grade-calculator")]
public class GradeCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolView("grade-calculator");
}

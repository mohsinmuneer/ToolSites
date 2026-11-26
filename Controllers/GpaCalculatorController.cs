using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("gpa-calculator")]
public class GpaCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("gpa-calculator");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("date-difference-calculator")]
public class DateDifferenceCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("date-difference-calculator");
}

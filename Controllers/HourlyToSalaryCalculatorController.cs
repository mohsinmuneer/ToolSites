using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("hourly-to-salary-calculator")]
public class HourlyToSalaryCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("hourly-to-salary-calculator");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("budget-calculator")]
public class BudgetCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("budget-calculator");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("discount-calculator")]
public class DiscountCalculatorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("discount-calculator");
}

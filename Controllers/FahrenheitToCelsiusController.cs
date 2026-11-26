using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("fahrenheit-to-celsius")]
public class FahrenheitToCelsiusController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("fahrenheit-to-celsius");
}

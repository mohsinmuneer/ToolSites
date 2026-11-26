using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("usd-to-pkr")]
public class UsdToPkrController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("usd-to-pkr");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("miles-to-km")]
public class MilesToKmController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("miles-to-km");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("hex-to-rgb")]
public class HexToRgbController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("hex-to-rgb");
}

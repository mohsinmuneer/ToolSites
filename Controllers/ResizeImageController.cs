using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("resize-image")]
public class ResizeImageController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("resize-image");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("webp-to-jpg")]
public class WebpToJpgController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("webp-to-jpg");
}

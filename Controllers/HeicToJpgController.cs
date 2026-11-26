using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("heic-to-jpg")]
public class HeicToJpgController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("heic-to-jpg");
}

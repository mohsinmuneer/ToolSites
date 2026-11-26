using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("png-to-jpg")]
public class PngToJpgController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("png-to-jpg");
}

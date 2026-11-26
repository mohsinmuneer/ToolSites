using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("compress-image")]
public class CompressImageController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("compress-image");
}

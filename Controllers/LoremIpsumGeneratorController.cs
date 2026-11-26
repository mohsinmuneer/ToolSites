using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("lorem-ipsum-generator")]
public class LoremIpsumGeneratorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("lorem-ipsum-generator");
}

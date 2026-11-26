using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("random-number-generator")]
public class RandomNumberGeneratorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("random-number-generator");
}

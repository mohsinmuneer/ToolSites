using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("word-counter")]
public class WordCounterController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("word-counter");
}

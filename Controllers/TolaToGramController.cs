using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("tola-to-gram")]
public class TolaToGramController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("tola-to-gram");
}

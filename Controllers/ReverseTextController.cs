using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("reverse-text")]
public class ReverseTextController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("reverse-text");
}

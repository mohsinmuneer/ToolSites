using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("json-formatter")]
public class JsonFormatterController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("json-formatter");
}

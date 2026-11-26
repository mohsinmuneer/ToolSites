using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("base64-encoder")]
public class Base64EncoderController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("base64-encoder");
}

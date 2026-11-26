using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("url-encoder-decoder")]
public class UrlEncoderDecoderController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("url-encoder-decoder");
}

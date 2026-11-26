using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("unix-timestamp-converter")]
public class UnixTimestampConverterController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("unix-timestamp-converter");
}

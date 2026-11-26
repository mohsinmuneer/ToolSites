using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("case-converter")]
public class CaseConverterController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("case-converter");
}

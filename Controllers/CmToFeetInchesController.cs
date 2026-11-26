using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("cm-to-feet-inches")]
public class CmToFeetInchesController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("cm-to-feet-inches");
}

using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("pounds-to-kg")]
public class PoundsToKgController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("pounds-to-kg");
}

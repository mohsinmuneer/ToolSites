using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

// Client-side tool: hisaab browser mein hota hai, server par file upload nahi hoti.
[Route("random-name-picker")]
public class RandomNamePickerController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolView("random-name-picker");
}

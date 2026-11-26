using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("passport-photo-maker")]
public class PassportPhotoMakerController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("passport-photo-maker");
}

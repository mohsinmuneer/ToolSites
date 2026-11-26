using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("password-generator")]
public class PasswordGeneratorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("password-generator");
}

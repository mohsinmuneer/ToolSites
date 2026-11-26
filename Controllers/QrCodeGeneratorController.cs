using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("qr-code-generator")]
public class QrCodeGeneratorController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("qr-code-generator");
}

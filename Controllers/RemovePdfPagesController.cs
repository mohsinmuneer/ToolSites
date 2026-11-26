using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("remove-pdf-pages")]
public class RemovePdfPagesController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("remove-pdf-pages");
}

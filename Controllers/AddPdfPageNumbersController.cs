using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("add-pdf-page-numbers")]
public class AddPdfPageNumbersController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolPage("add-pdf-page-numbers");
}

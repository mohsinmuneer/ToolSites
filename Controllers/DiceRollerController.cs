using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

// Client-side tool: hisaab browser mein hota hai, server par kuch upload nahi hota.
[Route("dice-roller")]
public class DiceRollerController : ToolBaseController
{
    [HttpGet("")]
    public IActionResult Index() => ToolView("dice-roller");
}

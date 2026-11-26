using Microsoft.AspNetCore.Mvc;

namespace ToolSite.Controllers;

public class PagesController : Controller
{
    private readonly IConfiguration _cfg;
    public PagesController(IConfiguration cfg) => _cfg = cfg;

    private IActionResult Page(string view, string title, string desc, string path)
    {
        var site = _cfg["Seo:SiteName"];
        ViewData["Title"] = $"{title} - {site}";
        ViewData["Description"] = desc;
        ViewData["Canonical"] = _cfg["Seo:BaseUrl"]!.TrimEnd('/') + path;
        return View(view);
    }

    [HttpGet("/about")] public IActionResult About() => Page("About", "About", "Who runs this site and how our free online tools work.", "/about");
    [HttpGet("/contact")] public IActionResult Contact() => Page("Contact", "Contact", "Contact us with questions, feedback or tool requests.", "/contact");
    [HttpGet("/privacy")] public IActionResult Privacy() => Page("Privacy", "Privacy Policy", "How we handle your data, cookies and advertising.", "/privacy");
    [HttpGet("/terms")] public IActionResult Terms() => Page("Terms", "Terms of Use", "Terms of use for our free online tools.", "/terms");
}

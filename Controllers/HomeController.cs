using System.Text;
using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

public class HomeController : Controller
{
    private readonly IConfiguration _cfg;
    public HomeController(IConfiguration cfg) => _cfg = cfg;

    private string BaseUrl => _cfg["Seo:BaseUrl"]!.TrimEnd('/');

    [HttpGet("/")]
    public IActionResult Index()
    {
        ViewData["Title"] = _cfg["Seo:SiteName"] + " - Free Online Calculators, PDF & Image Tools";
        ViewData["Description"] = "Free online calculators and tools: time, grade, dice roller, JPG to PDF and more. No sign-up. Files are not stored.";
        ViewData["Canonical"] = BaseUrl + "/";
        return View(ToolRegistry.Visible.ToList());
    }

    [HttpGet("/sitemap.xml")]
    public IActionResult Sitemap()
    {
        var sb = new StringBuilder("<?xml version=\"1.0\" encoding=\"UTF-8\"?><urlset xmlns=\"http://www.sitemaps.org/schemas/sitemap/0.9\">");
        foreach (var p in new[] { "", "about", "contact", "privacy", "terms" })
            sb.Append($"<url><loc>{BaseUrl}/{p}</loc></url>");
        foreach (var t in ToolRegistry.Visible) sb.Append($"<url><loc>{BaseUrl}/{t.Slug}</loc></url>");
        sb.Append("</urlset>");
        return Content(sb.ToString(), "application/xml", Encoding.UTF8);
    }

    [HttpGet("/robots.txt")]
    public IActionResult Robots() => Content($"User-agent: *\nAllow: /\nSitemap: {BaseUrl}/sitemap.xml\n", "text/plain");

    // AdSense approval ke baad publisher ID appsettings (Ads:PublisherId, jaise pub-1234567890123456) mein daalo.
    [HttpGet("/ads.txt")]
    public IActionResult AdsTxt()
    {
        var pub = _cfg["Ads:PublisherId"];
        if (string.IsNullOrWhiteSpace(pub)) return NotFound();
        return Content($"google.com, {pub}, DIRECT, f08c47fec0942fa0\n", "text/plain");
    }

    [HttpGet("/error")]
    public IActionResult Error()
    {
        ViewData["Title"] = "Something went wrong";
        ViewData["Description"] = "An unexpected error occurred.";
        ViewData["Canonical"] = BaseUrl + "/";
        return View();
    }
}

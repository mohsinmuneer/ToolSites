using Microsoft.AspNetCore.Mvc;
using ToolSite.Core;

namespace ToolSite.Controllers;

[Route("guides")]
public class GuidesController(IConfiguration configuration) : Controller
{
    public const string SplitPdfPath = "/guides/split-pdf-on-iphone";
    private const string SplitPdfTitle = "How to Split a PDF on iPhone";
    private const string SplitPdfDescription = "Use Safari to save selected pages from a PDF on iPhone. Includes a worked page-range example and how to find the download.";

    [HttpGet("")]
    public IActionResult Index()
    {
        SetMetadata("Tool Guides", "Step-by-step guides for using QuickTools to work with PDFs and other files.", "/guides");
        return View();
    }

    [HttpGet("split-pdf-on-iphone")]
    public IActionResult SplitPdfOnIphone()
    {
        if (ToolRegistry.IsHidden("split-pdf")) return NotFound();
        SetMetadata(SplitPdfTitle, SplitPdfDescription, SplitPdfPath);
        return View();
    }

    private void SetMetadata(string title, string description, string path)
    {
        ViewData["Title"] = title;
        ViewData["Description"] = description;
        ViewData["Canonical"] = configuration["Seo:BaseUrl"]!.TrimEnd('/') + path;
    }
}

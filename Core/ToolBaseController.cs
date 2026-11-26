using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace ToolSite.Core;

public abstract class ToolBaseController : Controller
{
    private static readonly string[] AllowedExt = { ".jpg", ".jpeg", ".png" };

    protected IActionResult ToolView(string slug)
    {
        if (ToolRegistry.IsHidden(slug)) return NotFound();
        var t = ToolRegistry.Find(slug) ?? throw new InvalidOperationException("Unknown tool: " + slug);
        var cfg = HttpContext.RequestServices.GetRequiredService<IConfiguration>();
        var baseUrl = cfg["Seo:BaseUrl"]!.TrimEnd('/');
        ViewData["Slug"] = t.Slug;
        ViewData["Title"] = t.Title;
        ViewData["Description"] = t.Description;
        ViewData["H1"] = t.H1;
        ViewData["Canonical"] = $"{baseUrl}/{t.Slug}";
        return View();
    }

    // Data-driven client-side tools: content ToolRegistry se, UI Views/ToolUi/<Ui>.cshtml se.
    protected IActionResult ToolPage(string slug)
    {
        var pre = ToolView(slug);   // sets ViewData; NotFound if the tool is hidden
        if (pre is NotFoundResult) return pre;
        var t = ToolRegistry.Find(slug)!;
        return View("~/Views/ToolPages/Tool.cshtml", t);
    }

    // Size, extension aur magic-bytes check. Error message wapas karta hai, sahi ho to null.
    protected async Task<string?> ValidateImagesAsync(IReadOnlyList<IFormFile>? files)
    {
        var limits = HttpContext.RequestServices.GetRequiredService<IOptions<LimitsOptions>>().Value;
        if (files == null || files.Count == 0) return "Please choose at least one image.";
        if (files.Count > limits.MaxFiles) return $"You can upload up to {limits.MaxFiles} images at once.";
        foreach (var f in files)
        {
            if (f.Length == 0) return "One of the files is empty.";
            if (f.Length > limits.MaxFileMB * 1024L * 1024L) return $"Each image must be under {limits.MaxFileMB} MB.";
            if (!AllowedExt.Contains(Path.GetExtension(f.FileName).ToLowerInvariant())) return "Only JPG and PNG images are supported.";
            var head = new byte[4];
            await using var s = f.OpenReadStream();
            var n = await s.ReadAsync(head, 0, 4);
            var jpg = n >= 3 && head[0] == 0xFF && head[1] == 0xD8 && head[2] == 0xFF;
            var png = n >= 4 && head[0] == 0x89 && head[1] == 0x50 && head[2] == 0x4E && head[3] == 0x47;
            if (!jpg && !png) return "A file is not a valid JPG or PNG image.";
        }
        return null;
    }
}

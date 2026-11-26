using MigraDocCore.DocumentObjectModel.MigraDoc.DocumentObjectModel.Shapes;
using PdfSharpCore;
using PdfSharpCore.Drawing;
using PdfSharpCore.Pdf;
using PdfSharpCore.Utils;
using SixLabors.ImageSharp.PixelFormats;

namespace ToolSite.Services;

public interface IJpgToPdfService
{
    byte[] Convert(IEnumerable<byte[]> images, string pageSize);
}

public class JpgToPdfService : IJpgToPdfService
{
    public byte[] Convert(IEnumerable<byte[]> images, string pageSize)
    {
        if (ImageSource.ImageSourceImpl == null)
            ImageSource.ImageSourceImpl = new ImageSharpImageSource<Rgba32>();

        using var doc = new PdfDocument();
        foreach (var bytes in images)
        {
            using var img = XImage.FromStream(() => new MemoryStream(bytes));
            var page = doc.AddPage();
            page.Size = pageSize == "Letter" ? PageSize.Letter : PageSize.A4;
            if (img.PixelWidth > img.PixelHeight)
            {   // landscape image: page ki width/height badal do
                var w = page.Width; page.Width = page.Height; page.Height = w;
            }
            double pw = page.Width.Point, ph = page.Height.Point, margin = 20;
            double scale = Math.Min((pw - 2 * margin) / img.PixelWidth, (ph - 2 * margin) / img.PixelHeight);
            double dw = img.PixelWidth * scale, dh = img.PixelHeight * scale;
            using var gfx = XGraphics.FromPdfPage(page);
            gfx.DrawImage(img, (pw - dw) / 2, (ph - dh) / 2, dw, dh);
        }
        using var ms = new MemoryStream();
        doc.Save(ms, false);
        return ms.ToArray();
    }
}

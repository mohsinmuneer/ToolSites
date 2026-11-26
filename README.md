# ToolSite v2 (ASP.NET Core 8 MVC)

## Chalane ke liye (.NET 8 SDK chahiye)
    dotnet add package PdfSharpCore     # sirf jpg-to-pdf ke liye, ek baar
    dotnet run
Phir terminal mein jo URL dikhe (jaise https://localhost:5001) uspar jao.

## Tools (40)
Browser mein chalte hain (server par kuch upload nahi hota): calculators, converters, text tools, image tools, PDF tools.
Server par sirf /jpg-to-pdf chalta hai (quota + rate limit ka example, PdfSharpCore chahiye).
Browser libraries (wwwroot/js/lib): pdf-lib (MIT), pdf.js (Apache-2.0), heic2any (MIT).

## Tools (purani list)
- /time-calculator, /grade-calculator, /dice-roller (browser mein chalte hain)
- /jpg-to-pdf (server par, quota + rate limit ke saath)
- Pages: /about /contact /privacy /terms, plus /sitemap.xml /robots.txt /ads.txt

## Tools chhupana / dikhana (phased launch)
appsettings.json -> "Tools": { "Hidden": [ ... ] }. Hidden tool 404 deta hai aur home, sitemap aur related links se hat jata hai.
Abhi 40 launch-ready tools visible hain (poora content Core/ToolContent.cs mein). Naye tool ka content tayyar ho to uska slug Hidden se nikaal do.

## Naya tool banane ka tareeqa
1. Controllers/XyzController.cs ([Route("xyz")], ToolView("xyz")) - TimeCalculatorController jaisa
2. Views/Xyz/Index.cshtml - h1, tool, <partial name="_AdSlot" model='"SlotTop"' />, 250-400 lafz ka asli content, _Faq, _RelatedTools
3. Core/ToolRegistry.cs mein ek line (title max 60, description max 155)
Sitemap, home grid aur related links khud update hote hain.

## appsettings.json mein badlo
- Seo:SiteName, Seo:BaseUrl (asli domain), Site:Owner, Site:ContactEmail
- Ads:Enabled=true aur Ads:PublisherId (pub-...) AdSense approval ke BAAD, saath mein SlotTop/SlotBottom ids
- Limits:DailyFreeQuota

## Production se pehle
- Privacy/Terms/About pages ko apne asal haalat ke hisaab se review karo aur likho
- Har tool ka content khud unique likho (template text mat chhodo)
- Redis quota (abhi MemoryCache hai, restart par reset)
- ForwardedHeaders: sirf Cloudflare/Nginx ke peeche chalao
- Europe/UK traffic ho to Google ka certified consent banner (CMP) lagao
- Docker: `docker build -t toolsite .`  (HTTPS proxy/Cloudflare par terminate karo)

## Preview all 55 tools locally

In PowerShell, run:

```powershell
$env:ASPNETCORE_ENVIRONMENT = 'Development'
$env:Tools__PreviewAll = 'true'
dotnet run --no-launch-profile
```

The preview shows all registered tools while production uses the `Tools:Hidden` list. The QR generator uses qrcode-generator 1.4.4 (MIT), with the license in `wwwroot/js/lib`.


See [US launch priorities](docs/us-launch.md) for the launch order, verification settings and AdSense setup.


## US launch batch reviewed November 26, 2026

Split PDF, Compress PDF, GPA Calculator, Tip Calculator and Discount Calculator are visible in the default configuration. The default directory now contains 15 tools. Development preview still includes all 50.

Run `dotnet build ToolSite.csproj`, `node tests/tool-defaults.mjs` and `./tests/run-preview-smoke.ps1` from the project root. The browser checks use Chrome and a generated PDF fixture on local ports 5187 and 9223.

Owner review: confirm the GPA instructions against the intended school's scale and repeat-course rules. Confirm coupon stacking, checkout fees and the current tax figure with the store. Before deployment, open PDFs produced from a real scan and confirm small print is clear on a phone. This batch introduces no fixed tax rates or new third-party libraries.

## US launch batch two reviewed November 26, 2026

Days Until, Car Loan, Mortgage, Hourly to Salary, and Sales Tax Calculator passed content, structured data, formula, and 360px Chrome checks. The default directory has 20 visible tools. The preview contains all 50.

Owner checks before deployment: confirm the annual interest rate and net trade-in credit from the vehicle lender, and reconcile mortgage insurance, tax, insurance and escrow with the lender's Loan Estimate. Confirm the applicable sales tax rate and taxable amount with the local authority. Verify paid hours, paid weeks and deductions with payroll. The loan and tax presets are illustrative.

- [CFPB mortgage payment and escrow explanation](https://www.consumerfinance.gov/ask-cfpb/on-a-mortgage-whats-the-difference-between-my-principal-and-interest-payment-and-my-total-monthly-payment-en-1941/)

## US launch batch three reviewed November 26, 2026

PDF to JPG, PNG to JPG, JPG to PNG, WebP to JPG and HEIC to JPG passed conversion and 360px Chrome checks. The default directory contains 25 tools and the development preview contains 50.

The browser suite generates its PDF, PNG, JPEG and WebP fixtures. Set `TOOLSITE_HEIC_FIXTURE` to a local HEIC file to exercise HEIC conversion in the same suite. The launch run used the [libheif example](https://github.com/strukturag/libheif/blob/master/examples/example.heic). No external fixture is stored in ToolSite.

Owner checks before deployment: try a recent iPhone HEIC with rotation and check its color and orientation on the output. Inspect tiny text on the JPEG of an actual PDF scan. Check the destination's current accepted format, pixel dimensions and file size limit. The batch adds no new third-party libraries.

## US launch batch four reviewed November 26, 2026

Case Converter, JSON Formatter, Base64 Encoder, URL Encoder Decoder and Reverse Text are visible in the default directory. The default directory has 30 tools and development preview has all 50.

The JSON formatter validates syntax before changing whitespace, preserving numeric spelling, string escapes and duplicate keys. The browser tests cover the examples, invalid input and Unicode. Before deployment, verify clipboard copy on the final HTTPS origin and check that the destination expects standard Base64 or percent encoding as appropriate. This batch has no rate or legal facts to verify.

## US launch batch five reviewed November 26, 2026

CM to Feet and Inches, Inches to CM, Pounds to KG, Miles to KM and Fahrenheit to Celsius passed the worked-example and 360px Chrome checks. The default directory contains 35 tools and development preview contains 50.

The shared converter now identifies invalid or out-of-range input and keeps the last edited field when the manual exchange rate changes. The browser suite checks Tola to Gram and USD to PKR for regressions.

Owner checks before deployment: confirm whether a destination accepts decimal feet or separate feet and inches. For a fit, shipment or safety threshold, confirm the original measurement and the required rounding. This batch contains no live rates or laws.

- [NIST unit conversion factors](https://www.nist.gov/pml/special-publication-811/nist-guide-si-appendix-b-conversion-factors/nist-guide-si-appendix-b9)

## PDF suite expansion reviewed November 26, 2026

PDF to PowerPoint, Rotate PDF, Remove PDF Pages, Add PDF Page Numbers and Watermark PDF are visible in the default directory. The PowerPoint export makes one image slide per PDF page. See the [PDF roadmap](docs/pdf-tool-roadmap.md) for the remaining workflows.

The output tests reopen the generated files and inspect slide count, rotations, remaining page order and which sheets contain new labels. The browser bundle is PptxGenJS 4.0.1 (MIT). The PptxGenJS and bundled JSZip notices are in `wwwroot/js/lib`.

Before deployment, open the generated PPTX in desktop PowerPoint and inspect small type. Check footers and watermarks on documents with existing text near the placement areas. Review bookmarks and links after removing pages.

using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace AspCoreWebApplication.Pages
{
  public class RenderPdf : PageModel
  {
    private readonly ILogger<RenderPdf> _logger;

    public RenderPdf(ILogger<RenderPdf> logger)
    {
      _logger = logger;
    }

    public ActionResult OnGet()
    {
        return Page();
    }

    public ActionResult OnGetByUrl(string url)
    {
      if (string.IsNullOrEmpty(url))
        return Page();

      var printOptions = new PdfPrintOptions
      {
        DPI = 300,
        PaperSize = PdfPrintOptions.PdfPaperSize.A4,
        EnableJavaScript = true,
        GrayScale = false,
        MarginBottom = 0,
        MarginLeft = 0,
        MarginRight = 0,
        MarginTop = 0,
        FitToPaperWidth = true,
        Zoom = 50
      };

      var renderer = new HtmlToPdf(printOptions);
      var pdf = renderer.RenderUrlAsPdf(url);

      var pdfBinaryData = pdf.BinaryData;

      return new FileContentResult(pdfBinaryData, "application/pdf");
    }
  }
}

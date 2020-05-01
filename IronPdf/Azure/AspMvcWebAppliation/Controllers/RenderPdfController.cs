using System.Web.Mvc;
using IronPdf;

namespace AspMvcWebAppliation.Controllers
{
  public class RenderPdfController : Controller
  {
    public ActionResult Index()
    {
      return View();
    }

    public ActionResult ByUrl(string url)
    {
      if (string.IsNullOrEmpty(url))
        return RedirectToAction("Index");

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
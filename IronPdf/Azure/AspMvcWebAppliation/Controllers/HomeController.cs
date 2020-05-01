using System.Web.Mvc;
using IronPdf;

namespace AspMvcWebAppliation.Controllers
{
  public class HomeController : Controller
  {
    public ActionResult Index(bool? pdfForPage)
    {
      if (pdfForPage == true)
      {
        AspxToPdf.RenderThisPageAsPdf(AspxToPdf.FileBehavior.Attachment, "PdfFile.pdf");
      }

      return View();
    }
  }
}
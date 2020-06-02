using System;
using System.Web.UI;
using IronPdf;

namespace AspxWebApplication
{
  public partial class _Default : Page
  {
    protected void Page_Load(object sender, EventArgs e)
    {
      var pdfAttachment = string.Equals(Request.QueryString.Get("pdfAttachment"), "true", StringComparison.OrdinalIgnoreCase);
      if (pdfAttachment)
      {
        var options = new PdfPrintOptions
        {
          Header = new SimpleHeaderFooter
          {
            CenterText = "Invoice",
            DrawDividerLine = false,
            FontFamily = "Arial",
            FontSize = 12
          },
          Footer = new SimpleHeaderFooter
          {
            LeftText = "{date} - {time}",
            RightText = "Page {page} of {total-pages}",
            FontFamily = "Arial",
            FontSize = 12,
          },
        };

        AspxToPdf.RenderThisPageAsPdf(AspxToPdf.FileBehavior.Attachment, "PdfAttachment.pdf", options);
      }
    }
  }
}
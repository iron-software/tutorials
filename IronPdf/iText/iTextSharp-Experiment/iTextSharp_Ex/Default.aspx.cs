using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iText.IO.Image;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.IO.Font.Constants;
using iText.Html2pdf;
using iText.Layout.Properties;
using iText.Kernel.Geom;

namespace iTextSharp_Ex
{
    public partial class _Default : Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            //FileInfo file = new FileInfo(DEST);
            //if (!file.Directory.Exists) file.Directory.Create();
            //new _Default().CreatePdf(DEST);

            ExistingWebURL();
            HTMLString();
            Chart();
        }

        private void ExistingWebURL()
        {

            //Initialize PDF writer
            PdfWriter writer = new PdfWriter("wikipedia.pdf");
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);

            ConverterProperties properties = new ConverterProperties();
            properties.SetBaseUri("https://en.wikipedia.org/wiki/Portable_Document_Format");

            Document document = HtmlConverter.ConvertToDocument(new FileStream("Test_iText7_1.pdf", FileMode.Open), pdf, properties);

            Paragraph header = new Paragraph("HEADER")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(header);

            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                Rectangle pageSize = pdf.GetPage(i).GetPageSize();
                float x = pageSize.GetWidth() / 2;
                float y = pageSize.GetTop() - 20;
                document.ShowTextAligned(header, x, y, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
            }

            document.SetTopMargin(50);
            document.SetBottomMargin(50);

            document.Close();

        }

        private void HTMLString()
        {

            HtmlConverter.ConvertToPdf("< h1 > Hello iText7 </ h1 >", new FileStream("HtmlToPDF.pdf", FileMode.Create));
        }

        private void Chart()
        {

            //Initialize PDF writer
            PdfWriter writer = new PdfWriter("chart.pdf");
            //Initialize PDF document
            PdfDocument pdf = new PdfDocument(writer);

            ConverterProperties properties = new ConverterProperties();
            properties.SetBaseUri("https://bl.ocks.org/mbostock/4062006");

            Document document = HtmlConverter.ConvertToDocument(new FileStream("Test_iText7_1.pdf", FileMode.Open), pdf, properties);

            Paragraph header = new Paragraph("HEADER")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(16);
            document.Add(header);

            for (int i = 1; i <= pdf.GetNumberOfPages(); i++)
            {
                Rectangle pageSize = pdf.GetPage(i).GetPageSize();
                float x = pageSize.GetWidth() / 2;
                float y = pageSize.GetTop() - 20;
                document.ShowTextAligned(header, x, y, i, TextAlignment.LEFT, VerticalAlignment.BOTTOM, 0);
            }

            document.Close();
        }

    }
}
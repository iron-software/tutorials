using IronPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Xsl;

namespace IronPDF_Ex
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ExistingWebURL();
            HTMLString();
            Chart();

            IronPdf.AspxToPdf.RenderThisPageAsPdf();


        }

        private void ExistingWebURL()
        { 
            
            // Create a PDF from any existing web page
            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderUrlAsPdf("https://en.wikipedia.org/wiki/Portable_Document_Format");

            // Create a PDF from an existing HTML
            Renderer.PrintOptions.MarginTop = 50;  //millimeters
            Renderer.PrintOptions.MarginBottom = 50;
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            Renderer.PrintOptions.Header = new SimpleHeaderFooter()
            {
                CenterText = "{pdf-title}",
                DrawDividerLine = true,
                FontSize = 16
            };
            Renderer.PrintOptions.Footer = new SimpleHeaderFooter()
            {
                LeftText = "{date} {time}",
                RightText = "Page {page} of {total-pages}",
                DrawDividerLine = true,
                FontSize = 14
            };


            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;

            Renderer.PrintOptions.EnableJavaScript = true;
            Renderer.PrintOptions.RenderDelay = 500; //milliseconds

            PDF.SaveAs("wikipedia.pdf");

        }

        private void HTMLString()
        {
            // Render any HTML fragment or document to HTML
            var Renderer = new IronPdf.HtmlToPdf();
            var PDF = Renderer.RenderHtmlAsPdf("<h1>Hello IronPdf</h1>");

            Renderer.PrintOptions.Footer = new HtmlHeaderFooter() { HtmlFragment = "<div style='text-align:right'><em style='color:pink'>page {page} of {total-pages}</em></div>" };

            var OutputPath = "HtmlToPDF.pdf";
            PDF.SaveAs(OutputPath);
            // This neat trick opens our PDF file so we can see the result in our default PDF viewer
            //   System.Diagnostics.Process.Start(OutputPath);
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Screen;

        }

        private void XMLtoPDF(string XSLT, string XML)
        {
            XslCompiledTransform transform = new XslCompiledTransform();
            using(XmlReader reader = XmlReader.Create(new StringReader(XSLT)))  
            {
                transform.Load(reader);
            }

            StringWriter results = new StringWriter();
            using(XmlReader reader = XmlReader.Create(new StringReader(XML))) 
            {
                transform.Transform(reader, null, results);
            }

            IronPdf.HtmlToPdf Renderer = new IronPdf.HtmlToPdf();
            // options, headers and footers may be set there
            // Render our XML as a PDF via XSLT
            Renderer.RenderHtmlAsPdf(results.ToString()).SaveAs("XMLtoPDF.pdf");
        }
        private void Chart()
        {
            // Create a PDF Chart a live rendered dataset using d3.js and javascript
            var Renderer = new HtmlToPdf();
            var PDF = Renderer.RenderUrlAsPdf("https://bl.ocks.org/mbostock/4062006");

            Renderer.PrintOptions.PaperSize = PdfPrintOptions.PdfPaperSize.A4;
            Renderer.PrintOptions.PaperOrientation = PdfPrintOptions.PdfPaperOrientation.Landscape;
            PDF.SaveAs("chart.pdf");
        }
    }
}
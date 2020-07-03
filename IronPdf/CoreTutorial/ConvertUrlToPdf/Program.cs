using System;

namespace ConvertUrlToPdf
{
    class Program
    {
        static void Main(string[] args)
        {
            var render = new IronPdf.HtmlToPdf();
            var doc = render.RenderHtmlAsPdf("<h1>Hello IronPdf</h1>");
            doc.SaveAs($@"{AppDomain.CurrentDomain.BaseDirectory}\HtmlString.pdf");
        }
        static void RenderUrlToPdf()
        {
            var render = new IronPdf.HtmlToPdf();
            var doc = render.RenderUrlAsPdf("https://www.wikipedia.org/");
            doc.SaveAs($@"{AppDomain.CurrentDomain.BaseDirectory}\wiki.pdf");
        }

        static void RenderHtmlStringToPdf()
        {
            var render = new IronPdf.HtmlToPdf();
            var doc = render.RenderHtmlAsPdf("<h1>Hello IronPdf</h1>");
            doc.SaveAs($@"{AppDomain.CurrentDomain.BaseDirectory}\HtmlString.pdf");
        }
    }
}

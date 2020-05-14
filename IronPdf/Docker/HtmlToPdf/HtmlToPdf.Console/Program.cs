using System;
using System.IO;
using System.Linq;
using IronPdf;

namespace HtmlToPdf.Console
{
    class Program
    {
        static int Main(string[] args)
        {
            try
            {
                // print header
                System.Console.WriteLine($"IronPdf {typeof(Installation).Assembly.GetName().Version} sample: converts HTML to PDF");

                if (args == null || args.Length < 2 || args.Length > 3)
                {
                    // print CLI
                    var applicationFileName = Path.GetFileName(typeof(Program).Assembly.Location);

                    System.Console.WriteLine();
                    System.Console.WriteLine($"\tdotnet {applicationFileName} html pdf tempFolder?");
                    System.Console.WriteLine("\t\thtml - link or local path to HTML page");
                    System.Console.WriteLine("\t\tpdf - path to PDF file");
                    System.Console.WriteLine("\t\ttempFolder - path to temp folder (optional)");
                    System.Console.WriteLine("\texamples: ");
                    System.Console.WriteLine($"\t\tdotnet {applicationFileName} local.html local.pdf");
                    System.Console.WriteLine($"\t\tdotnet {applicationFileName} https://google.com google.com.pdf");
                    System.Console.WriteLine();

                    // exit with invalid args
                    return 1;
                }

                // get arguments
                var html = args[0];
                var pdf = Path.GetFullPath(args[1]);

                // get path to temp folder
                var tempFolder = Path.GetFullPath(args.Skip(2).FirstOrDefault() ?? Installation.TempFolderPath ?? Environment.CurrentDirectory);

                // print arguments
                System.Console.WriteLine($"Arguments:");
                System.Console.WriteLine($"\thtml={html}");
                System.Console.WriteLine($"\tpdf={pdf}");
                System.Console.WriteLine($"\ttempFolder={tempFolder}");
                System.Console.WriteLine();

                // set temp folder path
                Installation.TempFolderPath = tempFolder;

                // render HTML to PDF
                new IronPdf.HtmlToPdf()
                    .RenderUrlAsPdf(html)
                    .SaveAs(pdf);

                // print path to the PDF file
                System.Console.WriteLine($"Result saved to: {pdf}");

                // exit with success
                return 0;
            }
            catch (Exception e)
            {
                System.Console.WriteLine($"Error occured: {e.Message}");
                System.Console.WriteLine($"Error details: {e}");
            }

            // exit with error
            return -1;
        }
    }
}
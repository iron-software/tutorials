using System;
using System.IO;
using System.Linq;
using System.Web.Http;
using IronPdf;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionCoreApp
{
  public static class RenderPdfByUrl
  {
    [FunctionName("RenderPdfByUrl")]
    public static IActionResult Run(
      [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = null)]
      HttpRequest req,
      ILogger log)
    {
      try
      {
        log.LogInformation($"C# HTTP trigger {nameof(RenderPdfByUrl)} function processed a request.");

        if (!req.Query.ContainsKey("url"))
        {
          return new BadRequestErrorMessageResult("Pass query parameter 'url' for execution this function.");
        }

        var url = req.Query["url"].FirstOrDefault();
        if (string.IsNullOrEmpty(url))
        {
          return new BadRequestErrorMessageResult($"Invalid query parameter url={url}");
        }

        log.LogInformation($"Installation.TempFolderPath={Installation.TempFolderPath}");

        log.LogInformation($"Render pdf from url: {url}");

        var renderer = new HtmlToPdf();
        var htmlAsPdf = renderer.RenderUrlAsPdf(url);

        var resultFileName = Path.Combine(Installation.TempFolderPath, "IronPDF", "Results", $"{Path.GetFileNameWithoutExtension(Path.GetTempFileName())}.pdf");

        var resultsFolder = Path.GetDirectoryName(resultFileName);
        if (!Directory.Exists(resultsFolder))
        {
          log.LogInformation($"Create folder for results: {resultsFolder}");
          Directory.CreateDirectory(resultsFolder);
        }

        log.LogInformation($"Save pdf to: {resultFileName}");

        htmlAsPdf.SaveAs(resultFileName);

        return new PhysicalFileResult(resultFileName, "application/pdf");
      }
      catch (Exception e)
      {
        log.LogCritical(e, "Error while processing a request.");
      }

      return new InternalServerErrorResult();
    }
  }
}
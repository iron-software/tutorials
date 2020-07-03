using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;
using System.IO;
using TicketsApps.AppCode;
using TicketsApps.Models;

namespace TicketsApps.Controllers
{
    public class BookTicketController : Controller
    {
        private readonly IHostEnvironment _host;
        public BookTicketController(IHostEnvironment host)
        {
            _host = host;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ClientModel model)
        {
            if (ModelState.IsValid)
            {
                ClientServices.AddClient(model);
                return RedirectToAction("TicketView");
            }
            return View(model);
        }

        public ActionResult TicketView()
        {
            var rand = new Random();
            var client = ClientServices.GetClient();
            var ticket = new TicketModel()
            {
                TicketNumber = rand.Next(100000, 999999),
                TicketDate = DateTime.Now,
                Email = client.Email,
                Name = client.Name,
                Phone = client.Phone
            };

            return View(ticket);
        }

        [HttpPost]
        public ActionResult TicketView(TicketModel model)
        {
            IronPdf.Installation.TempFolderPath = $@"{_host.ContentRootPath}/irontemp/";
            IronPdf.Installation.LinuxAndDockerDependenciesAutoConfig = true;
            var html = this.RenderViewAsync("_TicketPdf", model);
            var ironPdfRender = new IronPdf.HtmlToPdf();
            var pdfDoc = ironPdfRender.RenderHtmlAsPdf(html.Result);
            var path = Path.Combine(
                  Directory.GetCurrentDirectory(), "wwwroot");
            //var images = pdfDoc.RasterizeToImageFiles($@"{path}\thumbnail_*.jpg", 100, 80);
            return File(pdfDoc.Stream.ToArray(), "application/pdf");
        }
    }
}
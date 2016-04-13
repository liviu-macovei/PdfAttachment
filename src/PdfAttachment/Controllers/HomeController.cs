using System;
using System.ComponentModel;
using System.IO;
using Microsoft.AspNet.Mvc;
using PdfAttachment.Helpers;

namespace PdfAttachment.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            var ceva= GetPdfByApprovalUnitId();
            var root = @"C:\temp\";
            new PdfAttacher().AddAttachments(root,root);            
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }

        private byte[] GetPdfByApprovalUnitId()
        {
            byte[] pdfBytes = null;

            var generator = new HtmlGenerator(new ApprovalUnit(), HtmlVersionType.AdminVersion);
            var html = generator.GetHtml();

            using (var ms = new MemoryStream())
            {
                var root = @"C:\temp\";
                try
                {
                    Printer.GeneratePdf(Path.Combine(root, "bin"), html, ms);
                }
                catch (Win32Exception)
                {
                    Printer.GeneratePdf(root, html, ms);
                }
                pdfBytes = ms.ToArray();
            }

            System.IO.File.WriteAllBytes(@"C:\temp\" + DateTime.Now.Ticks + ".pdf", pdfBytes);

            return pdfBytes;
        }
    }
}
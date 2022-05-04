using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;

namespace CV_Maker_with_RDLC_Report_.Net_Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        private IWebHostEnvironment _environment;

        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Print()
        {
            string imgParam = "";
            string imgPath = Path.Combine(_environment.WebRootPath, "Deep Blue.jpg");
            string rdlcPath = Path.Combine(_environment.WebRootPath, "Report", "Report1.rdlc");

            using (var bit = new Bitmap(imgPath))
            {
                using(var ms = new MemoryStream())
                {
                    bit.Save(ms, ImageFormat.Bmp);
                    imgParam = Convert.ToBase64String(ms.ToArray());
                }
            }

            var dt = new DataTable();
            var report = new LocalReport();
            report.DataSources.Add(new ReportDataSource("DataSet",dt));
            report.ReportPath = rdlcPath;
            var param = new[] {new ReportParameter("image", imgParam) };
            report.SetParameters(param);
            return File(report.Render("PDF"),"application/pdf","Rport.pdf");
        }

    }
}
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
            string imgPath = Path.Combine(_environment.WebRootPath, "698.JPG");
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

            var dt1 = T1();
            report.DataSources.Add(new ReportDataSource("DataSet",dt1));

            var dt2 = T2();
            report.DataSources.Add(new ReportDataSource("DataSet1",dt2));

            report.ReportPath = rdlcPath;
            var param = new[] {new ReportParameter("image", imgParam) };
            report.SetParameters(param);
            return File(report.Render("PDF"),"application/pdf","Report.pdf");
        }

        public DataTable T1()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Training_Title");
            dt.Columns.Add("Topic");
            dt.Columns.Add("Institute");
            dt.Columns.Add("Country");
            dt.Columns.Add("Year");

            DataRow row = dt.NewRow();
            row["Training_Title"] = "A";
            row["Topic"] = "B";
            row["Institute"] = "C";
            row["Country"] = "D";
            row["Year"] = "E";
            dt.Rows.Add(row);

            return dt;
        }

        public DataTable T2()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("Major");
            dt.Columns.Add("Institute");
            dt.Columns.Add("Result");
            dt.Columns.Add("PassYear");
            dt.Columns.Add("Duration");

            DataRow row = dt.NewRow();
            row["Title"] = "A";
            row["Major"] = "B";
            row["Institute"] = "C";
            row["Result"] = "D";
            row["PassYear"] = "E";
            row["Duration"] = "F";
            dt.Rows.Add(row);

            return dt;
        }

    }
}
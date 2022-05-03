
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Reporting.NETCore;
using RDLC_Report_in_.NET_Core_MVC.Data;
using RDLC_Report_in_.NET_Core_MVC.Models;
using System.Data;
using System.Drawing;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace RDLC_Report_in_.NET_Core_MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly AppDbContext _db;
        public HomeController(IWebHostEnvironment env, AppDbContext db)
        {
            _env = env;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Print()
        {
            string renderFormat = "PDF";
            string mimeType = "application/pdf";
            string extension = "pdf";

            string imgParam = "";
            string imgPath = Path.Combine(_env.WebRootPath,  "field-with-big-tree.jpg");

            using (var b = new Bitmap(imgPath))
            {
                using (var ms = new MemoryStream())
                {
                    b.Save(ms, ImageFormat.Bmp);
                    imgParam = Convert.ToBase64String(ms.ToArray());
                }
            }
            using var report = new LocalReport();
            var dt = await GetStudentList();
            report.DataSources.Add(new ReportDataSource("Student", dt));
            var param = new[] { 
                new ReportParameter("para1", "RDLC Repot from database") ,
                new ReportParameter("img", imgParam) 
            };
            report.ReportPath = Path.Combine(_env.WebRootPath, "Report", "Report1.rdlc");
            report.SetParameters(param);
            var pdf = report.Render(renderFormat);
            return File(pdf, mimeType, "report."+extension);
        }

        public async Task<DataTable> GetStudentList()
        {
            var dt = new DataTable();
            dt.Columns.Add("Role");
            dt.Columns.Add("Name");

            DataRow row;
            var data = await _db.studentD.ToListAsync();
            foreach (var stu in data)
            {
                row = dt.NewRow();
                row["Role"] = stu.Id;
                row["Name"] = stu.name;
                dt.Rows.Add(row);
            }

            return dt;
        }


    }
}

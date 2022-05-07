using CV_Maker_with_RDLC_Report_.Net_Core_MVC.Models;
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
        private static CV_Model _cv;
        private static string imageName;

        public HomeController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(CV_Model cv)
        {
            if (ModelState.IsValid)
            {
                _cv = cv;
                imageName = await ImageUploder(cv.imgFile);
                return RedirectToAction(nameof(Print));

            }
            return View(cv);
        }

        public IActionResult Print()
        {
            string imgParam = "";
            string imgPath = Path.Combine(_environment.WebRootPath, "Images", imageName);
            string rdlcPath = Path.Combine(_environment.WebRootPath, "Report", "Report1.rdlc");

            using (var bit = new Bitmap(imgPath))
            {
                using (var ms = new MemoryStream())
                {
                    bit.Save(ms, ImageFormat.Bmp);
                    imgParam = Convert.ToBase64String(ms.ToArray());
                }
            }

            var dt = new DataTable();
            var report = new LocalReport();

            var dt1 = Training_Table(_cv.trainingTitle, _cv.topic, _cv.institute, _cv.country, _cv.year);
            report.DataSources.Add(new ReportDataSource("DataSet", dt1));

            var dt2 = Varsity_Collage_Table(_cv.degree_title, _cv.major, _cv.varsity, _cv.result, _cv.passYear, _cv.duration,
                _cv.titleHS, _cv.majorHS, _cv.collage, _cv.resultHS, _cv.passYearHS, _cv.durationHS);
            report.DataSources.Add(new ReportDataSource("DataSet1", dt2));

            report.ReportPath = rdlcPath;
            var param = new[] { new ReportParameter("image", imgParam) };
            report.SetParameters(param);
            return File(report.Render("PDF"), "application/pdf", "Report.pdf");
        }

        public DataTable Training_Table(string Training_Title, string Topic, string Institute, string Country, string Year)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Training_Title");
            dt.Columns.Add("Topic");
            dt.Columns.Add("Institute");
            dt.Columns.Add("Country");
            dt.Columns.Add("Year");

            DataRow row = dt.NewRow();
            row["Training_Title"] = Training_Title;
            row["Topic"] = Topic;
            row["Institute"] = Institute;
            row["Country"] = Country;
            row["Year"] = Year;
            dt.Rows.Add(row);

            return dt;
        }

        public DataTable Varsity_Collage_Table(string degree_title, string major, string varsity, string result, string passYear, string duration,
            string titleHS, string majorHS, string collage, string resultHS, string passYearHS, string durationHS)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Title");
            dt.Columns.Add("Major");
            dt.Columns.Add("Institute");
            dt.Columns.Add("Result");
            dt.Columns.Add("PassYear");
            dt.Columns.Add("Duration");

            DataRow row;

            row = dt.NewRow();
            row["Title"] = degree_title;
            row["Major"] = major;
            row["Institute"] = varsity;
            row["Result"] = result;
            row["PassYear"] = passYear;
            row["Duration"] = duration;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row["Title"] = titleHS;
            row["Major"] = majorHS;
            row["Institute"] = collage;
            row["Result"] = resultHS;
            row["PassYear"] = passYearHS;
            row["Duration"] = durationHS;
            dt.Rows.Add(row);

            return dt;
        }
        public async Task<string> ImageUploder(IFormFile file)
        {
            string name = Path.GetFileNameWithoutExtension(file.FileName);
            string ext = Path.GetExtension(file.FileName);
            name = name + DateTime.Now.ToString("yy-MM-dd-hh-mm-ss") + ext;
            string path = Path.Combine(_environment.WebRootPath, "Images", name);
            using (var filestream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(filestream);
                return name;
            }
        }

    }
}
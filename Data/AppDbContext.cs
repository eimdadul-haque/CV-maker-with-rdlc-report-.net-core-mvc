using CV_Maker_with_RDLC_Report_.Net_Core_MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CV_Maker_with_RDLC_Report_.Net_Core_MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

        public DbSet<CV_Model>? cv_d { get; set; }
    }
}

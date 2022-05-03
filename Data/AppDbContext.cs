using Microsoft.EntityFrameworkCore;
using RDLC_Report_in_.NET_Core_MVC.Models;

namespace RDLC_Report_in_.NET_Core_MVC.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options)
        {

        }

      public DbSet<StudentModel> studentD { get; set; }
    }
}

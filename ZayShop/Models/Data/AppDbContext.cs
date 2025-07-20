using Microsoft.EntityFrameworkCore;

namespace ZayShop.Models.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<StudentModel> Studentss { get; set; }
        public DbSet<SliderModel> Sliders { get; set; }
    }
}
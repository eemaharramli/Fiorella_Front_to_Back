using Fiorella.Models;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderImage> SliderImages { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<Reason> Reasons { get; set; }
        public DbSet<Expert> Experts { get; set; }
        public DbSet<ExpertJob> ExpertJobs { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<Thing> Things { get; set; }
        public DbSet<InstagramImage> InstagramImages { get; set; }
    }
}
using Microsoft.EntityFrameworkCore;

namespace Fiorella.DataAccessLayer
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
         
    }
}
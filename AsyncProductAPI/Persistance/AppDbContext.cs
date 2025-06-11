using AsyncProductAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AsyncProductAPI.Persistance
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options):base(options)
        {
            
        }

        public DbSet<ListProduct> Products { get; set; }
    }
}

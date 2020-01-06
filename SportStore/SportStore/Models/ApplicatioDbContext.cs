
using Microsoft.EntityFrameworkCore;

namespace SportStore.Models
{
    public class ApplicatioDbContext : DbContext
    {
        public ApplicatioDbContext(DbContextOptions<ApplicatioDbContext> options) :
            base (options)
        {
        }

        public DbSet<Product> Products { get; set; }

    }
}

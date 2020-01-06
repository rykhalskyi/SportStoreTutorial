using System.Linq;

namespace SportStore.Models
{
    public class EFProductRepository : IProductRepository
    {
        ApplicatioDbContext _context;

        public EFProductRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

        public IQueryable<Product> Products => _context.Products;
    }
}

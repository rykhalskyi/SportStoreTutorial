using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportStore.Models
{
    public class EFOrderRepository : IOrderRepository
    {
        ApplicatioDbContext _context;

        public EFOrderRepository(ApplicatioDbContext context)
        {
            _context = context;
        }

        public IQueryable<Order> Orders => _context.Orders
            .Include(o => o.Lines)
            .ThenInclude(l => l.Product);

        public void SaveOrder(Order order)
        {
            _context.AttachRange(order.Lines.Select(l => l.Product));
            if (order.OrderId == 0)
            {
                _context.Orders.Add(order);
            }

            _context.SaveChanges();
        }
    }
}

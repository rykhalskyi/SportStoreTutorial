using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportStore.Models
{
    public class Cart
    {
        List<CartLine> _lines = new List<CartLine>();

        public IEnumerable<CartLine> Lines => _lines;

        public virtual void AddItem(Product product, int quantity)
        {
            var line = _lines.FirstOrDefault(l => l.Product.ProductId == product.ProductId);

            if (line == null)
            {
                _lines.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public virtual void RemoveLine(Product product)
        {
            _lines.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal TotalValue => _lines.Sum(l => l.Product.Price * l.Quantity);
        
        public virtual void Clear()
        {
            _lines.Clear();
        }
    }
}

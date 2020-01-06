using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System.Linq;

namespace SportStore.Controllers
{
    public class ProductController : Controller
    {
        IProductRepository _repository;

        public int PageSize { get; set; } = 4;

        public ProductController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult List(int productPage = 1) 
        {
           return View(_repository.Products
               .OrderBy(p=>p.ProductId)
               .Skip((productPage-1)* PageSize)
               .Take(PageSize));
        }

    }
}
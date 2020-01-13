using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using System.Linq;

namespace SportStore.Controllers
{
    public class AdminController : Controller
    {
        IProductRepository _repository;

        public AdminController(IProductRepository repository)
        {
            _repository = repository;
        }

        public ViewResult Index()
        {
            return View(_repository.Products);
        }

        public ViewResult Edit(int ProductId)
        {
            return View(_repository.Products.FirstOrDefault(p => p.ProductId == ProductId))
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using SportStore.Models;
using SportStore.Models.ViewModels;
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

        public ViewResult List(string category, int productPage = 1) 
        {
            var viewModel = new ProductListViewModel
            {
                Products = _repository.Products
                 .Where(p => category == null || p.Category == category)
                 .OrderBy(p => p.ProductId)
                 .Skip((productPage - 1) * PageSize)
                 .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = productPage,
                    ItemsPerPage = PageSize,
                    TotalItems = category == null ? _repository.Products.Count() :
                    _repository.Products.Count(x=>x.Category == category)
                },
                CurrentCategory = category
            };

           return View(viewModel);
        }

    }
}
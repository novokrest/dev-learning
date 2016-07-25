using SportsStore.Domain.Abstract;
using SportsStore.WebUI.Models;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductRepository _repository;

        public ProductController(IProductRepository productRepository)
        {
            _repository = productRepository;
            PageSize = 4;
        }

        public int PageSize { get; set; }

        public ViewResult List(int page = 1)
        {
            var viewModel = new ProductListViewModel
            {
                Products = _repository.Products
                                      .OrderBy(p => p.ProductID)
                                      .Skip((page - 1) * PageSize)
                                      .Take(PageSize),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = PageSize,
                    TotalItems = _repository.Products.Count()
                }
            };

            return View(viewModel);
        }
    }
}
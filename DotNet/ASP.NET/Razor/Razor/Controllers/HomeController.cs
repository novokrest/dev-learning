using Razor.Models;
using System.Web.Mvc;

namespace Razor.Controllers
{
    public class HomeController : Controller
    {
        private readonly Product _product = new Product
        {
            ProductID = 1,
            Name = "Kayak",
            Description = "A boat for one person",
            Category = "Watersports",
            Price = 275M
        };
        
        // GET: Home
        public ActionResult Index()
        {
            return View(_product);
        }

        public ActionResult NameAndPrice()
        {
            return View(_product);
        }

        public ActionResult DemoExpression()
        {
            ViewBag.ProductCount = 1;
            ViewBag.ExpressShip = true;
            ViewBag.ApplyDiscount = false;
            ViewBag.Supplier = null;

            return View(_product);
        }

        public ActionResult DemoArray()
        {
            Product[] products =
            {
                new Product { Name = "Kayak", Price = 275M },
                new Product { Name = "Lifejacket", Price = 48.95M },
                new Product { Name = "Soccer ball", Price = 19.50M },
                new Product { Name = "Cornet flag", Price = 34.95M }
            };

            return View(products);
        }
    }
}
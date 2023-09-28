using Microsoft.AspNetCore.Mvc;
using WebApplication2.Controllers;
using WebApplication2.Services;

namespace WebApplication2.Areas.ProductManage.Controllers
{
    [Area("ProductManage")]
    [Route("ListProduct")]
    public class ProductController : Controller
    {
        private readonly ProductService _productService;

        private readonly ILogger<PlanetController> _logger;

        public ProductController(ProductService productService, ILogger<PlanetController> logger)
        {
            _productService = productService;
            _logger = logger;
        }

        [Route("ProductName")]
        public IActionResult Index()
        {
            // Trả về 1 list các sản phẩm SẮP XẾP (OrderBy) theo Name
            var products = _productService.OrderBy(p => p.Name).ToList();

            return View(products);
        }
    }
}

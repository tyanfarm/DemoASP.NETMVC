using Microsoft.AspNetCore.Mvc;
using WebApplication2.Services;

namespace WebApplication2.Controllers
{
    public class TestController : Controller
    {
        // Logger để thông báo trên terminal
        private readonly ILogger<TestController> _logger;

        // môi trường web (để xuất ảnh)
        private readonly IWebHostEnvironment _webHostEnvironment;

        private readonly ProductService _productService;

        // Tiêm thông qua constructor, DI Container sẽ cấp 1 Singleton cài đặt trong Program.cs
        public TestController(ILogger<TestController> logger, IWebHostEnvironment webHostEnvironment, ProductService productService)
        {
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
            _productService = productService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowInfo()
        {
            var content = @"
                abc,
                bdakda,
                poasdka
                ";

            return Content(content, "text/plain");  
        }

        public IActionResult DisplayImg()
        {
            string webRootPath = _webHostEnvironment.WebRootPath;
            string imagePath = Path.Combine(webRootPath, "images", "ashisland.jpeg");

            var bytes = System.IO.File.ReadAllBytes(imagePath);

            return File(bytes, "image/jpeg");
        }

        public IActionResult JsonData()
        {
            return Json(
                new
                {
                    name = "Tyan",
                    age = 20
                }
            );
        }

        // Chuyển hướng từ Test/Privacy qua Home/Privacy
        public IActionResult Privacy()
        {
            // (Action, Controller)
            var url = Url.Action("Privacy", "Home");
            _logger.LogInformation("Chuyen huong den " + url);

            return LocalRedirect(url);  // local ~ host
        }

        // Redirect to link online

        public IActionResult Google()
        {
            var url = "https://www.google.com";
            _logger.LogInformation("Chuyen huong den " + url);

            return Redirect(url);  // local ~ host
        }

        // ViewResult | View()
        public IActionResult ViewResult(string username)
        {
            // View --> Razor Page, read file .cshtml

            //// View(template)
            //return View("/MyView/Test1.cshtml");

            //// View(template, model)
            //if (string.IsNullOrEmpty(username))
            //{
            //    username = "Guest";
            //}
            //return View("/MyView/Test1.cshtml", username);

            //// Views/ControllerName/Action.cshtml -- ViewResult.cshtml
            //// object để cho biết nó là model
            //return View((object)username);

            return View("Test1",username);
        }

        [TempData]
        public string StatusMessage { get; set; }

        //[AcceptVerbs("POST")]
        public IActionResult ViewProduct(int? id)
        {
            var product = _productService.Where(p => p.Id == id).FirstOrDefault();

            if (product == null)
            {
                StatusMessage = "The product you requested was not found";

                // tạo ra một URL tới action "Index" của controller "Home".
                return Redirect(Url.Action("Index", "Home"));
            }

            //// Views/Test/ViewProduct.cshtml
            //// MyView/Test/ViewProduct.cshtml
            //return View(product);

            ViewData["product"] = product;
            ViewData["ID"] = product.Id;

            return View();
        }
    }
}

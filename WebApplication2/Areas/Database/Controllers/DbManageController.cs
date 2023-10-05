using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication2.Models;

namespace WebApplication2.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    public class DbManageController : Controller
    {
        private readonly AppDbContext _dbContext;

        public DbManageController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            return View();
        }

        [HttpPost]
        // Vì là hàm bất đồng bộ nên cần thêm Task<>
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();

            // Ở Index.cshtml ta đặt <partial name="_AlertMessage" /> để truyền StatusMessage
            StatusMessage = success ? "Delete Successfully" : "The action can't be complete";

            // Khi đặt nameof thì sau này khi đổi tên action sẽ được tự đổi ở đây
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> CreateDb()
        {
            // Kiểm tra xem cơ sở dữ liệu đã tồn tại chưa. Nếu chưa tồn tại, nó sẽ tạo cơ sở dữ liệu.
            await _dbContext.Database.MigrateAsync();

            // Ở Index.cshtml ta đặt <partial name="_AlertMessage" /> để truyền StatusMessage
            StatusMessage = "Update Successfully" ;

            // Khi đặt nameof thì sau này khi đổi tên action sẽ được tự đổi ở đây
            return RedirectToAction(nameof(Index));
        }
    }
}

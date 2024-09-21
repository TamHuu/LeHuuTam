using LeHuuTam.Data;
using LeHuuTam.Models;
using Microsoft.AspNetCore.Mvc;

namespace LeHuuTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AccountController(ILogger<AccountController> logger, ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var result = _dbContext.Users.ToList();
            return View(result);
        }
      

    }
}

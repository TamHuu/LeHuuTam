using LeHuuTam.Data;
using LeHuuTam.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeHuuTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthenticationController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public AuthenticationController(ILogger<AuthenticationController> logger, ApplicationDbContext context)
        {
            _dbContext = context;
        }
        [Authorize("Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            var model = new Users();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(Users model)
        {
            if (ModelState.IsValid)
            {
                var user = await _dbContext.Users.FirstOrDefaultAsync(m => m.UserName == model.UserName);

                if (user != null && user.Password == model.Password)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid username or password");
                }
            }

            return View();
        }


        [HttpGet]
        public IActionResult Register()
        {
            var model = new Users();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Register(Users model)
        {
            if (ModelState.IsValid)
            {
                var result = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == model.UserName);
                if (result == null)
                {
                    _dbContext.Users.Add(model);
                    await _dbContext.SaveChangesAsync();
                    return RedirectToAction("Login", "Authentication");
                }
                else
                {
                    ModelState.AddModelError("", "This username already exists");
                }
            }

            return View(model);
        }

    }
}

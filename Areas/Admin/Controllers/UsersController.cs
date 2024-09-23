using LeHuuTam.Data;
using LeHuuTam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeHuuTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public UsersController(ILogger<UsersController> logger, ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var result = _dbContext.Users.ToList();
            return View(result);
        }
        [HttpGet]
        public JsonResult GetUserById(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }
            return Json(new { success = true, data = user });
        }

        [HttpPost]
        public JsonResult UpdateUser(int id, string userName, string password, string email)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            user.UserName = userName;
            user.Password = password;
            user.Email = email;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();  

            return Json(new { success = true, data = user });
        }
        [HttpDelete]
        public JsonResult DeleteUser(int id)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            try
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges(); 

                return Json(new { success = true, message = "User deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting user: " + ex.Message });
            }
        }
        [HttpPost]
        public JsonResult AddUser(string userName, string password, string email)
        {
            var user = new Users();
        
            user.UserName = userName;
            user.Password = password;
            user.Email = email;

            _dbContext.Users.Update(user);
            _dbContext.SaveChanges();

            return Json(new { success = true, data = user });
        }


    }
}

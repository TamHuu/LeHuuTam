using LeHuuTam.Data;
using LeHuuTam.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LeHuuTam.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public ProductsController(ILogger<UsersController> logger, ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public IActionResult Index()
        {
            var result = _dbContext.Products.ToList();
            return View(result);
        }
        [HttpGet]
        public JsonResult GetProductsById(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(u => u.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "Product not found" });
            }
            return Json(new { success = true, data = product });
        }
        [HttpPost]
        public JsonResult AddProduct(string Brand, string Model, string Price, string Description, string Stock, string ImageUrl)
        {
            var products = new Products();

            products.Brand = Brand;
            products.Model = Model;

            products.Price = decimal.Parse(Price);
            products.Description = Description;
            products.Stock = int.Parse(Stock);
            products.ImageUrl = ImageUrl;

            _dbContext.Products.Add(products);
            _dbContext.SaveChanges();

            return Json(new { success = true, data = products });
        }




        //[HttpPost]
        //public JsonResult UpdateProductById(int id)
        //{
        //    var product = _dbContext.Products.FirstOrDefault(u => u.Id == id);
        //    if (product == null)
        //    {
        //        return Json(new { success = false, message = "User not found" });
        //    }

        //    Products.UserName = userName;
        //    Products.Password = password;
        //    Products.Email = email;

        //    _dbContext.Users.Update(Products);
        //    _dbContext.SaveChanges();

        //    return Json(new { success = true, data = Products });
        //}
        [HttpDelete]
        public JsonResult DeleteProducts(int id)
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



    }
}

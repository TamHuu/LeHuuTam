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

        // Xem
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

        // Thêm
        [HttpPost]
        public JsonResult AddProduct(string Brand, string Model, string Price, string Description, string Stock, string ImageUrl)
        {
            var existingProduct = _dbContext.Products.FirstOrDefault(u => u.Brand == Brand);

            if (existingProduct != null)
            {
                return Json(new { success = false, message = "Sản phẩm này đã tồn tại" });
            }
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
        // Sửa
        [HttpPut]
        public JsonResult UpdateProduct(int id, string Brand, string Model, string Price, string Description, string Stock, string ImageUrl)
        {
            var product = _dbContext.Products.FirstOrDefault(u => u.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "User not found" });
            }

            product.Brand = Brand;
            product.Model = Model;
            product.Price = decimal.Parse(Price);
            product.Description = Description;
            product.Stock = int.Parse(Stock);
            product.ImageUrl = ImageUrl;

            _dbContext.Products.Update(product);
            _dbContext.SaveChanges();

            return Json(new { success = true, data = product });
        }
        [HttpDelete]
        public JsonResult DeleteProduct(int id)
        {
            var product = _dbContext.Products.FirstOrDefault(u => u.Id == id);
            if (product == null)
            {
                return Json(new { success = false, message = "product not found" });
            }

            try
            {
                _dbContext.Products.Remove(product);
                _dbContext.SaveChanges();

                return Json(new { success = true, message = "product deleted successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error deleting product: " + ex.Message });
            }
        }



    }
}

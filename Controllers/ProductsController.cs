using LeHuuTam.Areas.Admin.Controllers;
using LeHuuTam.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace LeHuuTam.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<ProductsController> _logger;

        // Constructor
        public ProductsController(ILogger<ProductsController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        // Action to get the product list
        [HttpGet]
        public IActionResult Index()
        {
            var result = _dbContext.Products.ToList();
            return View(result);
        }
    }
}

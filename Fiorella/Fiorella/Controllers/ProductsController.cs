using System.Linq;
using Fiorella.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class ProductsController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly int _productsCount;
        
        public ProductsController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
            _productsCount = this._dbContext.Products.Count();
        }
        
        public IActionResult Index()
        {
            ViewBag.ProductsCount = _productsCount;
            var products = this._dbContext.Products.Include(x=>x.Category).Take(4).ToList();
            
            return View(products);
        }

        public IActionResult Load(int skip)
        {
            if (skip >= _productsCount)
            {
                return BadRequest();
            }
            var products = this._dbContext.Products.Include(x => x.Category).Skip(skip).Take(4).ToList();

            return PartialView("_ProductPartial", products);
        }
    }
}
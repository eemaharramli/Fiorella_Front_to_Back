using System.Linq;
using System.Threading.Tasks;
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
            
            return View();
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

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this._dbContext.Products.SingleOrDefaultAsync(x=> x.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
    }
}
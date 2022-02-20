using System;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ProductController : Controller
    {
        private readonly AppDbContext _dbContext;

        public ProductController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
        public async Task<IActionResult> Index(int page = 1)
        {
            ViewBag.totalPage = Math.Ceiling((decimal)this._dbContext.Products.Count() / 2);
            ViewBag.currentPage = page;

            var products = await this._dbContext.Products.Include(x => x.Category).Skip((page - 1) / 2).Take(2)
                .ToListAsync();

            // var products = await this._dbContext.Products.Include(x=>x.Category).ToListAsync();
            return View(products);
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var product = await this._dbContext.Products.Include(x=>x.Category).Where(x=>x.Id == id).FirstOrDefaultAsync();

            if (product == null)
            {
                return NotFound();
            }
            
            return View(product);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Product product)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var isExistProduct =
                await this._dbContext.Products.AnyAsync(x => x.Name.ToLower().Trim() == product.Name.ToLower().Trim());

            if (isExistProduct)
            {
                ModelState.AddModelError("Name", "The Product with this name is already exist");
                return View();
            }

            await this._dbContext.AddAsync(product);
            await this._dbContext.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
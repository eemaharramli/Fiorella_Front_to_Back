using System.Collections.Generic;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class CategoryController : Controller
    {
        private readonly AppDbContext _dbContext;

        public CategoryController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await this._dbContext.Categories.ToListAsync();

            return View(categories);
        }

        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return BadRequest();
            }
            
            var category = await this._dbContext.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }
            
            return View(category);
        }
        
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var isExistCategory = await this._dbContext.Categories.AnyAsync(x =>
                x.Name.ToLower().Trim() == category.Name.ToLower().Trim());
            
            if (isExistCategory)
            {
                ModelState.AddModelError("Name", "The Category with this name is already exist");
                return View();
            }

            await this._dbContext.Categories.AddAsync(category);
            await this._dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            var category = await this._dbContext.Categories.FindAsync(id);
        
            if (category == null)
            {
                return BadRequest();
            }
            
            return View(category);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Category category)
        {
            if (id == null)
            {
                return NotFound();
            }
        
            if (id != category.Id)
            {
                return BadRequest();
            }
        
            if (!ModelState.IsValid)
            {
                return View();
            }
        
            var existCategory = await this._dbContext.Categories.FindAsync(id);
        
            if (existCategory == null)
            {
                return NotFound();
            }
        
            var isExist =
                await this._dbContext.Categories.AnyAsync(
                    x => x.Name.Trim().ToLower() == category.Name.Trim().ToLower() &&
                                 x.Id != id);
        
            if (isExist)
            {
                ModelState.AddModelError("Name", "There is already Category with the same name");
                return View();
            }
        
            existCategory.Name = category.Name;
            existCategory.Description = category.Description;
            
            await this._dbContext.SaveChangesAsync();
        
        
            return RedirectToAction(nameof(Index));
        }


        
        
        

        public IActionResult Delete(int? id)
        {
            return View();
        }
    }
}
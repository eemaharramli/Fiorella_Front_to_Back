using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class ExpertController : Controller
    {
        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public ExpertController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            this._dbContext = dbContext;
            this._environment = environment;
        }
        
        // GET
        public async Task<IActionResult> Index()
        {
            var experts = await this._dbContext.Experts.ToListAsync();

            if (experts == null)
            {
                return NotFound();
            }

            return View(experts);
        }

        // GET
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await this._dbContext.Experts.FindAsync(id);

            if (expert == null)
            {
                return BadRequest();
            }

            return View(expert);
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, Expert expert)
        {
            if (id == null)
            {
                return NotFound();
            }

            if (id != expert.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                return View();
            }

            var existExpert = await this._dbContext.Experts.FindAsync(id);

            if (existExpert == null)
            {
                return NotFound();
            }

            var isExistExpert = await this._dbContext.Experts.AnyAsync(
                x => x.Fullname.Trim().ToLower() == expert.Fullname.Trim().ToLower() && 
                            x.Id != id);

            if (isExistExpert)
            {
                ModelState.AddModelError("Fullname", $"There is already Expert with name {expert.Fullname}");
                return View();
            }

            existExpert.Fullname = expert.Fullname;

            await this._dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await this._dbContext.Experts.Include(x=>x.ExpertJob).Where(x=>x.Id == id).FirstOrDefaultAsync();
            if (expert == null)
            {
                return BadRequest();
            }
            
            return View(expert);
        }

        //GET
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await this._dbContext.Experts.FindAsync(id);

            if (expert == null)
            {
                return BadRequest();
            }

            return View(expert);
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteExpert(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expert = await this._dbContext.Experts.FindAsync(id);

            if (expert == null)
            {
                return BadRequest();
            }

            this._dbContext.Remove(expert);
            await this._dbContext.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }

        //GET
        public IActionResult Create()
        {
            return View();
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expert expert)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            
            var isExistExpert =
                await this._dbContext.Experts.AnyAsync(x => x.Fullname.ToLower().Trim() == expert.Fullname.ToLower().Trim());

            if (isExistExpert)
            {
                ModelState.AddModelError("Name", $"The Expert with name {expert.Fullname} is already exist");
                return View();
            }

            var webRootPath = this._environment.WebRootPath;
            var fileName = $"{Guid.NewGuid()}_{expert.Photo.FileName}";
            var path = Path.Combine(webRootPath, "img", fileName);
            var fileStream = new FileStream(path, FileMode.CreateNew);
            
            await expert.Photo.CopyToAsync(fileStream);

            expert.Image = fileName;
            await this._dbContext.Experts.AddAsync(expert);
            await this._dbContext.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
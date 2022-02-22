using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.Areas.AdminPanel.Data;
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
                return PartialView("_NotFoundPartial");
            }

            return View(experts);
        }

        // GET
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return PartialView("_NotFoundPartial");
            }

            var expert = await this._dbContext.Experts.FindAsync(id);

            if (expert == null)
            {
                return PartialView("_InternalErrorPartial");
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
                return PartialView("_NotFoundPartial");
            }

            if (id != expert.Id)
            {
                return PartialView("_InternalErrorPartial");
            }
            
            var existExpert = await this._dbContext.Experts.FindAsync(id);

            if (existExpert == null)
            {
                return PartialView("_NotFoundPartial");
            }
            
            if (!ModelState.IsValid)
            {
                return View(existExpert);
            }
            
            if (!expert.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must upload an image");
                return View(existExpert);
            }

            if (!expert.Photo.IsAllowedSize(2))
            {
                ModelState.AddModelError("Photo", "Your image must be less than 2mb");
                return View(existExpert);
            }

            var path = Path.Combine(Constants.ImageFolderPath, existExpert.Image);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var fileName = await expert.Photo.GenerateFile(Constants.ImageFolderPath);

            expert.Image = fileName;

            var isExistExpert = await this._dbContext.Experts.AnyAsync(
                x => x.Fullname.Trim().ToLower() == expert.Fullname.Trim().ToLower() && 
                            x.Id != id);

            if (isExistExpert)
            {
                ModelState.AddModelError("Fullname", $"There is already Expert with name {expert.Fullname}");
                return View(existExpert);
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

            if (!expert.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must upload an image");
                return View();
            }

            if (!expert.Photo.IsAllowedSize(2))
            {
                ModelState.AddModelError("Photo", "The image size must be less than 2mb");
                return View();
            }
            
            var isExistExpert =
                await this._dbContext.Experts.AnyAsync(x => x.Fullname.ToLower().Trim() == expert.Fullname.ToLower().Trim());

            if (isExistExpert)
            {
                ModelState.AddModelError("Name", $"The Expert with name {expert.Fullname} is already exist");
                return View();
            }

            var fileName = await expert.Photo.GenerateFile(Constants.ImageFolderPath);

            expert.Image = fileName;
            await this._dbContext.Experts.AddAsync(expert);
            await this._dbContext.SaveChangesAsync();
            
            return RedirectToAction(nameof(Index));
        }
    }
}
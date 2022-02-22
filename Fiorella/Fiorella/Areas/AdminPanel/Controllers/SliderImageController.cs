using System;
using System.IO;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class SliderImageController : Controller
    {

        private readonly AppDbContext _dbContext;
        private readonly IWebHostEnvironment _environment;

        public SliderImageController(AppDbContext dbContext, IWebHostEnvironment environment)
        {
            this._dbContext = dbContext;
            this._environment = environment;
        }
        
        public async Task<IActionResult> Index()
        {
            var sliderImages = await this._dbContext.SliderImages.ToListAsync();
            
            return View(sliderImages);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(SliderImage sliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (!sliderImage.Photo.ContentType.Contains("image"))
            {
                ModelState.AddModelError("Photo", "You must upload an image");
                return View();
            }

            if (sliderImage.Photo.Length > 4096 * 1000)
            {
                ModelState.AddModelError("Photo", "The picture size must be not more than 2048 kb");
                return View();
            }

            var webRootPath = this._environment.WebRootPath;
            var fileName = $"{Guid.NewGuid()}_{sliderImage.Photo.FileName}";
            var path = Path.Combine(webRootPath, "img", fileName);
            var fileStream = new FileStream(path, FileMode.CreateNew);
            
            await sliderImage.Photo.CopyToAsync(fileStream);

            sliderImage.SliderImageName = fileName;
            await this._dbContext.SliderImages.AddAsync(sliderImage);
            await this._dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var sliderImage = await this._dbContext.SliderImages.FindAsync(id);
            
            if (sliderImage == null)
            {
                return NotFound();
            }
            
            return View(sliderImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteCategory(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sliderImage = await this._dbContext.SliderImages.FindAsync(id);
            if (sliderImage == null)
            {
                return BadRequest();
            }

            this._dbContext.SliderImages.Remove(sliderImage);
            await this._dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
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
        // public async Task<IActionResult> Create(SliderImage sliderImage)
        // {
        //     var existSliderImagesCount = this._dbContext.SliderImages.Count();
        //
        //     if (existSliderImagesCount >= 5)
        //     {
        //         ModelState.AddModelError("Photo", "To add more images delete old images first");
        //         return View();
        //     }
        //     
        //     if (!ModelState.IsValid)
        //     {
        //         return View();
        //     }
        //
        //     if (!sliderImage.Photo.IsImage())
        //     {
        //         ModelState.AddModelError("Photo", "You must upload an image");
        //         return View();
        //     }
        //
        //     if (!sliderImage.Photo.IsAllowedSize(2))
        //     {
        //         ModelState.AddModelError("Photo", "The picture size must be not more than 2048 kb");
        //         return View();
        //     }
        //
        //     var fileName = await sliderImage.Photo.GenerateFile(Constants.ImageFolderPath);
        //
        //     sliderImage.SliderImageName = fileName;
        //     await this._dbContext.SliderImages.AddAsync(sliderImage);
        //     await this._dbContext.SaveChangesAsync();
        //
        //     return RedirectToAction(nameof(Index));
        // }

        public async Task<IActionResult> Create(SliderImage sliderImage)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            foreach (var image in sliderImage.Photos)
            {
                var existSliderImagesCount = this._dbContext.SliderImages.Count();
                
                if (existSliderImagesCount >= 5)
                {
                    ModelState.AddModelError("Photos", "To add more images delete old images first");
                    return View();
                }
                
                if (!image.IsImage())
                {
                    ModelState.AddModelError("Photos", $"{image.Name} - You must upload an image");
                    return View();
                }
        
                if (!image.IsAllowedSize(2))
                {
                    ModelState.AddModelError("Photos", $"{image.Name} - The picture size must be not more than 2048 kb");
                    return View();
                }
        
                var fileName = await image.GenerateFile(Constants.ImageFolderPath);

                var newSliderImage = new SliderImage {SliderImageName = fileName};
                await this._dbContext.SliderImages.AddAsync(newSliderImage);
                await this._dbContext.SaveChangesAsync();
            }
            
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
                return PartialView("_NotFoundPartial");
            }
            
            return View(sliderImage);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteSliderImage(int? id)
        {
            if (id == null)
            {
                return PartialView("_NotFoundPartial");
            }

            var existSliderImage = await this._dbContext.SliderImages.FindAsync(id);

            if (existSliderImage == null)
            {
                return PartialView("_NotFoundPartial");
            }

            var path = Path.Combine(Constants.ImageFolderPath, existSliderImage.SliderImageName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }
            
            this._dbContext.Remove(existSliderImage);
            await this._dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        //GET
        public async Task<IActionResult> Update(int? id)
        {
            if (id == null)
            {
                return PartialView("_NotFoundPartial");
            }

            var sliderImage = await this._dbContext.SliderImages.FirstOrDefaultAsync(x => x.Id == id);
            if (sliderImage == null)
            {
                return PartialView("_NotFoundPartial");
            }

            return View(sliderImage);
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id, SliderImage sliderImage)
        {
            if (id == null)
            {
                return PartialView("_NotFoundPartial");
            }

            if (id != sliderImage.Id)
            {
                return PartialView("_InternalErrorPartial");
            }

            var existSliderImage = await this._dbContext.SliderImages.FindAsync(id);

            if (existSliderImage == null)
            {
                return PartialView("_NotFoundPartial");
            }

            if (!ModelState.IsValid)
            {
                return View(sliderImage);
            }
            
            if (!sliderImage.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "You must upload an image");
                return View(sliderImage);
            }

            if (!sliderImage.Photo.IsAllowedSize(2))
            {
                ModelState.AddModelError("Photo", "Your image must be less than 2mb");
                return View(sliderImage);
            }

            var path = Path.Combine(Constants.ImageFolderPath, existSliderImage.SliderImageName);

            if (System.IO.File.Exists(path))
            {
                System.IO.File.Delete(path);
            }

            var fileName = await sliderImage.Photo.GenerateFile(Constants.ImageFolderPath);
            existSliderImage.SliderImageName = fileName;
            await this._dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
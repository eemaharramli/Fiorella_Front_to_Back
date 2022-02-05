using System.Linq;
using Fiorella.DataAccessLayer;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IActionResult Index()
        {
            var sliderImages = this._dbContext.SliderImages.ToList();
            var slider = this._dbContext.Sliders.SingleOrDefault();
            
            return View(new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider
            });
        }
    }
}
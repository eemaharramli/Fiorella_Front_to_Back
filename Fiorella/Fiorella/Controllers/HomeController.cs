using System;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Http;
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
            HttpContext.Session.SetString("session", "session value");

            Response.Cookies.Append("cookies", "cookie value", new CookieOptions{Expires = DateTimeOffset.Now.AddHours(1)});

            var sliderImages = this._dbContext.SliderImages.ToList();
            var slider = this._dbContext.Sliders.SingleOrDefault();
            var categories = this._dbContext.Categories.ToList();
            var products = this._dbContext.Products.Include(x=>x.Category).Take(4).ToList();
            var about = this._dbContext.Abouts.Include(x => x.Reason).ToList();
            var reason = this._dbContext.Reasons.ToList();
            var expert = this._dbContext.Experts.Include(x => x.ExpertJob).ToList();
            var expertJob = this._dbContext.ExpertJobs.ToList();
            var blog = this._dbContext.Blogs.ToList();
            var thing = this._dbContext.Things.ToList();
            var instagramImages = this._dbContext.InstagramImages.ToList();
            var subscribe = this._dbContext.Subscribes.ToList();
            var bio = this._dbContext.Bios.ToList();

            return View(new HomeViewModel
            {
                SliderImages = sliderImages,
                Slider = slider,
                Categories = categories,
                Products = products,
                About = about,
                Reasons = reason,
                Experts = expert,
                ExpertJobs = expertJob,
                Blogs = blog,
                Things = thing,
                InstagramImages = instagramImages,
                Subscribes = subscribe,
                Bios = bio
            });
        }

        public async Task<IActionResult> Search(string searchedProduct)
        {
            if (string.IsNullOrEmpty(searchedProduct))
            {
                return NoContent();
            }

            var products = await this._dbContext.Products
                .Where(x => x.Name.ToLower().Contains(searchedProduct.ToLower())).ToListAsync();

            return PartialView("_SearchedProductPartial", products);
        }


        public IActionResult Basket()
        {
            var session = HttpContext.Session.GetString("session");
            var cookie = Request.Cookies["cookies"];


            return Content(session + " - " + cookie);
        }
    }
}
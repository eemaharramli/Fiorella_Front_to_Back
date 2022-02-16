using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            ViewBag.basketTotalCount = 0;
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
        
        public async Task<IActionResult> Basket()
        {
            var basket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(basket))
            {
                return Content("Empty basket");
            }
            
            var basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
            var newBasket = new List<BasketViewModel>();
            foreach (var basketViewModel in basketViewModels)
            {
                var product = await this._dbContext.Products.FindAsync(basketViewModel.Id);
                if (product == null)
                {
                    continue;
                }
                newBasket.Add(new BasketViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Image = product.Image,
                    Count = basketViewModel.Count,
                    Price = product.Price,
                });
            }

            basket = JsonConvert.SerializeObject(newBasket);
            Response.Cookies.Append("basket", basket);

            double totalPrice = 0;

            foreach (var item in newBasket)
            {
                totalPrice += item.Price * item.Count;
            }

            ViewData["totalPrice"] = totalPrice;
            
            return View(newBasket);
        }

        public async Task<IActionResult> AddToBasket(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var product = await this._dbContext.Products.FindAsync(id);
            
            if (product == null)
            {
                return NotFound();
            }
            
            List<BasketViewModel> basketViewModels;
            
            var existBasket = Request.Cookies["basket"];
            if (string.IsNullOrEmpty(existBasket))
            {
                basketViewModels = new List<BasketViewModel>();
            }
            else
            {
                basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(existBasket);
            }

            var existBasketViewModel = basketViewModels.FirstOrDefault(x => x.Id == id);
            
            if (existBasketViewModel == null)
            {
                existBasketViewModel = new BasketViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Image = product.Image,
                    Price = product.Price
                };
                
                basketViewModels.Add(existBasketViewModel);
            }
            else
            {
                existBasketViewModel.Count++;
            }

            var basket = JsonConvert.SerializeObject(basketViewModels);
            Response.Cookies.Append("basket", basket);

            return Json(basketViewModels);
        }

        public async Task<IActionResult> AddProductCount(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var product = await this._dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var existBasket = Request.Cookies["basket"];
            List<BasketViewModel> basketViewModels;

            if (string.IsNullOrEmpty(existBasket))
            {
                basketViewModels = new List<BasketViewModel>();
            }
            else
            {
                basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(existBasket);
            }

            var existBasketViewModel = basketViewModels.FirstOrDefault(x => x.Id == id);
            if (existBasketViewModel == null)
            {
                basketViewModels.Add(new BasketViewModel
                {
                    Id = product.Id,
                    Name = product.Name,
                    Image = product.Image,
                    Price = product.Price
                });
            }
            else
            {
                existBasketViewModel.Count++;
            }

            double totalCount = 0;
            
            foreach (var item in basketViewModels)
            {
                totalCount += item.Price * item.Count;
            }

            ViewData["totalCount"] = totalCount;
            
            var basket = JsonConvert.SerializeObject(basketViewModels);
            Response.Cookies.Append("basket", basket);

            return PartialView("_BasketPartial", basketViewModels);
        }

        public async Task<IActionResult> RemoveProductCount(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            
            var product = await this._dbContext.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            var existBasket = Request.Cookies["basket"];
            List<BasketViewModel> basketViewModels;

            if (string.IsNullOrEmpty(existBasket))
            {
                basketViewModels = new List<BasketViewModel>();
            }
            else
            {
                basketViewModels = JsonConvert.DeserializeObject<List<BasketViewModel>>(existBasket);
            }

            var existBasketViewModel = basketViewModels.FirstOrDefault(x => x.Id == id);
            if (existBasketViewModel == null)
            {
                return NotFound();
            }
            
            existBasketViewModel.Count--;

            var basket = JsonConvert.SerializeObject(basketViewModels);
            
            Response.Cookies.Append("basket", basket);
            
            double totalCount = 0;
            
            foreach (var item in basketViewModels)
            {
                totalCount += item.Price * item.Count;
            }
            
            ViewData["totalCount"] = totalCount;

            return PartialView("_BasketPartial", basketViewModels);
        }
    }
}
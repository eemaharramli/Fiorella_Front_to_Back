using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Fiorella.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public HeaderViewComponent(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            int count = 0;
            double price = 0;
            var basket = Request.Cookies["basket"];
            
            if (!string.IsNullOrEmpty(basket))
            {
                var products = JsonConvert.DeserializeObject<List<BasketViewModel>>(basket);
                if (products == null)
                {
                    products = new List<BasketViewModel>();
                }
                
                count = products.Count;
                
                foreach (var item in products)
                {
                    price += item.Price * item.Count;
                }
                ViewBag.BasketCount = count;
                ViewBag.Total = price;
            }

            var bio = await this._dbContext.Bios.SingleOrDefaultAsync();

            return View(bio);
        }
    }
}
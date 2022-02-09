using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.ViewComponents
{
    public class ProductViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ProductViewComponent(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync(int take = 8)
        {
            var products = await this._dbContext.Products.Include(x=>x.Category).Take(take).ToListAsync();

            return View(products);
        }
    }
}
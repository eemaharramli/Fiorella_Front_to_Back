using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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
            var bio = await this._dbContext.Bios.SingleOrDefaultAsync();
            
            return View(bio);
        }
    }
}
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public FooterViewComponent(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var bio = this._dbContext.Bios.ToListAsync();

            return View(bio);
        }

    }
}
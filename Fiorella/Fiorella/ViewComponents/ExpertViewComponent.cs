using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.ViewComponents
{
    public class ExpertViewComponent : ViewComponent
    {
        private readonly AppDbContext _dbContext;

        public ExpertViewComponent(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {

            var experts = this._dbContext.Experts.Include(x => x.ExpertJob).ToListAsync();
            
            return View(experts);
        }
    }
}
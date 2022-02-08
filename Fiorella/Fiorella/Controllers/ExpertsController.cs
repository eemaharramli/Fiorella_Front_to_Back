using System.Linq;
using Fiorella.DataAccessLayer;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class ExpertsController : Controller
    {

        private readonly AppDbContext _dbContext;

        public ExpertsController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
        public IActionResult Index()
        {
            var experts = this._dbContext.Experts.Include(x=>x.ExpertJob).ToList();

            return View(experts);
        }
    }
}
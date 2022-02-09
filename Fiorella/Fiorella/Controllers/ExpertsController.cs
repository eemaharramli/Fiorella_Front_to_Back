using Fiorella.DataAccessLayer;
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
            return View();
        }
    }
}
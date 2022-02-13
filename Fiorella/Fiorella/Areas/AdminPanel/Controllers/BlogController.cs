using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class BlogController : Controller
    {
        private readonly AppDbContext _dbContext;

        public BlogController(AppDbContext dbContext)
        {
            this._dbContext = dbContext;
        }
        
        public async Task<IActionResult> Index()
        {
            var blogs = await this._dbContext.Blogs.ToListAsync();
            
            return View(blogs);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }

            var blog = await this._dbContext.Blogs.FirstOrDefaultAsync(x => x.id == id);

            if (blog == null)
            {
                return NotFound();
            }
            
            return View(blog);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var isExistBlog = await this._dbContext.Blogs.AnyAsync(x =>
                x.Title.Trim().ToLower() == blog.Title.Trim().ToLower());

            if (isExistBlog)
            {
                ModelState.AddModelError("Title", "The Blog List with this title is already exist.");
                return View();
            }

            await this._dbContext.AddAsync(blog);
            await this._dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
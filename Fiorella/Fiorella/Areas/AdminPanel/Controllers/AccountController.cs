using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fiorella.DataAccessLayer;
using Fiorella.Models;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Fiorella.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly AppDbContext _dbContext;
        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, AppDbContext dbContext)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await this._userManager.Users.ToListAsync();
            var roles = await this._roleManager.Roles.ToListAsync();
            var userRoles = await this._dbContext.UserRoles.ToListAsync();

            if (users == null && roles == null && userRoles == null)
            {
                return PartialView("_NotFoundPartial");
            }

            return View(users);
        }
        
        //GET
        public async Task<IActionResult> ChangePassword(string id)
        {
            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return PartialView("_NotFoundPartial");
            }
            
            return View();
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePassword model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Try again");
                
                return View();
            }

            var user = await this._userManager.FindByNameAsync(model.Username);
            if (user == null)
            {
                return BadRequest();
            }

            var result = await this._userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }

            return RedirectToAction(nameof(Index), "Dashboard");
        }

        // GET
        public async Task<IActionResult> ChangeUserActivity(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_NotFoundPartial");
            }

            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return PartialView("_NotFoundPartial");
            }
            
            return View(user);
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName("ChangeUserActivity")]
        public async Task<IActionResult> ChangeUserActivityStatus(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_NotFoundPartial");
            }

            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return PartialView("_NotFoundPartial");
            }

            switch (user.IsDeleted)
            {
                case false:
                    user.IsDeleted = true;
                    break;
                case true:
                    user.IsDeleted = false;
                    break;
            }

            await this._userManager.UpdateAsync(user);

            return RedirectToAction(nameof(Index));
        }
        
        // GET
        public async Task<IActionResult> ChangeRole(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return PartialView("_NotFoundPartial");
            }

            var user = await this._userManager.FindByIdAsync(id);
            if (user == null)
            {
                return PartialView("_NotFoundPartial");
            }

            var roles = await this._userManager.GetRolesAsync(user);
            if (roles == null)
            {
                return PartialView("_NotFoundPartial");
            }

            ViewBag.ActualRole = roles.FirstOrDefault();
            
            return View();
        }
        
        // POST
        public async Task<IActionResult> ChangeRole(string id, UserManagerViewModel userManagerViewModel) // ?
        {
            return RedirectToAction(nameof(Index));
        }
    }
}
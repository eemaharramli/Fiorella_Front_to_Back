using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        
        public AccountController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
        }

        //GET
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var users = await this._userManager.Users.ToListAsync();

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

         //POST
         // [HttpPost]
         // [ValidateAntiForgeryToken]
         // public async Task<IActionResult> ChangePassword(ChangePassword model)
         // {
         //     if (!ModelState.IsValid)
         //     {
         //         ModelState.AddModelError("", "Incorrect");
         //         return View(model);
         //     }
         //     
         //     var user = await this._userManager.FindByNameAsync(model.Username);
         //     
         //     var password = await this._userManager.CheckPasswordAsync(user, model.OldPassword);
         //     if (!password)
         //     {
         //         ModelState.AddModelError("", "Wrong inputs of old credentials");
         //         return View();
         //     }
         //     
         //     var result = await this._userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
         //     if (!result.Succeeded)
         //     {
         //         foreach (var error in result.Errors)
         //         {
         //             ModelState.AddModelError(String.Empty, error.Description);
         //         }
         //     
         //         return View();
         //     }
         //     
         //     return RedirectToAction(nameof(Index));
         // }
    }
}
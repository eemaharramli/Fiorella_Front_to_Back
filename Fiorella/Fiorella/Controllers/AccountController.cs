using System;
using System.Threading.Tasks;
using Fiorella.Models;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fiorella.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        //GET
        public IActionResult Register()
        {
            return View();
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(ResgisterViewModel registerModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await this._userManager.FindByNameAsync(registerModel.Username);
            if (existUser != null)
            {
                return View();
            }

            var user = new User
            {
                Fullname = registerModel.Fullname,
                UserName = registerModel.Username,
                Email = registerModel.Email
            };

            var result = await this._userManager.CreateAsync(user, registerModel.Password);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View();
            }

            // await _signInManager.SignInAsync(user, false);

            return RedirectToAction(nameof(Login));
        }
        
        //GET
        public IActionResult Login()
        {
            return View();
        }
        
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var existUser = await _userManager.FindByNameAsync(loginModel.Username);
            if (existUser == null)
            {
                ModelState.AddModelError("", "Wrong Credentials");
                
                return View();
            }

            // var pass = await this._userManager.GeneratePasswordResetTokenAsync(existUser);

            var result = await _signInManager.PasswordSignInAsync(existUser.UserName, loginModel.Password, loginModel.KeepMeSignedIn, true);
            if (result.IsLockedOut)
            {
                ModelState.AddModelError("", $"Contact with administration. Your user is locked. Lock out end time - {existUser.LockoutEnd}");
                
                return View();
            }
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Wrong Credentials for password");
                
                return View();
            }
            
            return RedirectToAction(nameof(Index), "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await this._signInManager.SignOutAsync();
            
            return RedirectToAction(nameof(Login), "Account");
        }
        
        //GET
        public IActionResult ForgotPassword()
        {
            return View();
        }
        
        //POST
        


        #region oldUserActions

        // // GET
        // public IActionResult Register()
        // {
        //     return View();
        // }
        //
        // // POST
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Register(ResgisterViewModel registerModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View();
        //     }
        //
        //     // var existUser = await this._userManager.FindByNameAsync(registerModel.Username);
        //     // if (existUser != null)
        //     // {
        //     //     ModelState.AddModelError("Username", $"A user with name {registerModel.Username} is already exists");
        //     //     
        //     //     return View();
        //     // }
        //
        //     var user = new User
        //     {
        //         Email = registerModel.Email,
        //         UserName = registerModel.Username,
        //         Fullname = registerModel.Fullname
        //     };
        //     var result = await this._userManager.CreateAsync(user, registerModel.Password);
        //
        //     if (!result.Succeeded)
        //     {
        //         foreach (var error in result.Errors)
        //         {
        //             ModelState.AddModelError("", error.Description);
        //         }
        //
        //         return View();
        //     }
        //
        //     await this._signInManager.SignInAsync(user, true);
        //     return RedirectToAction(nameof(Login));
        // }
        //
        // //GET
        // public IActionResult Login()
        // {
        //     return View();
        // }
        //
        // //POST
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Login(LoginViewModel loginModel)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return View();
        //     }
        //
        //     var existUser = await this._userManager.FindByNameAsync(loginModel.Username);
        //     if (existUser == null)
        //     {
        //         ModelState.AddModelError("", "Wrong credentials");
        //         
        //         return View();
        //     }
        //
        //     // var result = await this._userManager.CheckPasswordAsync(existUser, loginModel.Password);
        //     // if (result == null)
        //     // {
        //     //     ModelState.AddModelError("", "Wrong credentials");
        //     //     return View();
        //     // }
        //
        //     var result = await this._signInManager.PasswordSignInAsync(existUser, loginModel.Password, false, true);
        //     if (!result.Succeeded)
        //     {
        //         ModelState.AddModelError("", "Wrong credentials");
        //         
        //         return View();
        //     }
        //     
        //     
        //     return RedirectToAction("Index", "Home");
        // }
        //
        // public async Task<IActionResult> Logout()
        // {
        //     await this._signInManager.SignOutAsync();
        //     
        //     return RedirectToAction(nameof(Index), "Home");
        // }
        //
        // //GET
        // public async Task<IActionResult> ForgotPassword()
        // {
        //     
        //     
        //     return View();
        // }
        //
        // //POST
        // public async Task<IActionResult> ForgetPassword(ResgisterViewModel resgisterViewModel)
        // {
        //     return RedirectToAction(nameof(Login));
        // }

        #endregion
    }
}
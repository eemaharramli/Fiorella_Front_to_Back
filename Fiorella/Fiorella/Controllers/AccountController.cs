using System;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Threading.Tasks;
using Fiorella.Models;
using Fiorella.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Newtonsoft.Json;

namespace Fiorella.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

    public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ILogger<AccountController> logger)
        {
            this._userManager = userManager;
            this._signInManager = signInManager;
            this._logger = logger;
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

            var token = await this._userManager.GenerateEmailConfirmationTokenAsync(user);
            var link = Url.Action(nameof(VerifyEmail), "Account", new {email = user.Email, token}, Request.Scheme,
                Request.Host.ToString());

            MailMessage message = new MailMessage();
            message.From = new MailAddress("codep320@gmail.com", "Fiorello");
            message.To.Add(user.Email);
            string body = string.Empty;
            using (StreamReader reader = new StreamReader("wwwroot/assets/template/verifyemail.html"))
            {
                body = await reader.ReadToEndAsync();
            }

            message.Body = body.Replace("{{link}}", link);
            message.Subject = "VerifyEmail";
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient();
            client.Host = "smtp.gmail.com";
            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new NetworkCredential("codep320@gmail.com", "codeacademyp320");
            client.Send(message);
            TempData["confirm"] = true;
            
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(model.Email);
                if (user != null && await this._userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await this._userManager.GeneratePasswordResetTokenAsync(user);
                    var resetLink = Url.Action("ResetPassword", "Account", new {email = model.Email, token}, Request.Scheme);
                    this._logger.Log(LogLevel.Warning, resetLink);

                    return View("ForgotPasswordConfirmation");
                }

                return View("ForgotPasswordConfirmation");
            }
            
            return View(model);
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ResetPassword(string token, string email)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Something get wrong");
                
                return View();
            }
            
            if (token == null || email == null)
            {
                ModelState.AddModelError("", "Something get wrong. Try to get password reset link once more");
            }
            
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await this._userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await this._userManager.ResetPasswordAsync(user, model.Token, model.Password);
                    if (result.Succeeded)
                    {
                        return View("ResetPasswordConfirmation");
                        
                        
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }

                    return View(model);
                }

                return View("ResetPasswordConfirmation");
            }

            return View(model);
        }


        public async Task<IActionResult> VerifyEmail(string email, string token)
        {
            var user = await this._userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return BadRequest();
            }

            await this._userManager.ConfirmEmailAsync(user, token);
            await this._signInManager.SignInAsync(user, true);
            TempData["confirmed"] = true;

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
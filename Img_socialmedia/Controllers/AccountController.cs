using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using System;

namespace Img_socialmedia.Controllers
{
    public class AccountController : Controller
    {
        private readonly db_shutterContext _context;
        private readonly SignInManager<UserViewModel> _signInManager;
        private readonly UserManager<UserViewModel> _userManager;
        private readonly ILogger<RegisterViewModel> _logger;

        public AccountController(db_shutterContext context, UserManager<UserViewModel> userManager, SignInManager<UserViewModel> signInManager, ILogger<RegisterViewModel> logger)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }



        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login([FromRoute] string returnUrl = "")
        {
            var model = new LoginViewModel { ReturnUrl = returnUrl };
            return View(model);
        }


        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model )
        { 
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.email, model.password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.email);
                    await _userManager.UpdateAsync(user);

                    _logger.LogInformation(1, "user logged in");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "login failed");
                    return View(model);
                }

            }
            return RedirectToAction("Index", "Login");
        }

       
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Signup(RegisterViewModel model)
        {
       
            ViewBag.showtopbar = false;
            if (ModelState.IsValid) 
            { 
                var user = new UserViewModel
                {
                    Firstname = model.first_name,
                    Lastname = model.last_name,
                    Username = model.username,                   
                    
                };

                var result = await _userManager.CreateAsync(user, model.password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    _logger.LogInformation(3, "User created a new account with password.");
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
 
            }
           
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation(4, "user logged out");
            return RedirectToAction("Index", "Home");
        }
    }
}
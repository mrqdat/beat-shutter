using Img_socialmedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Img_socialmedia.Controllers
{
    public class AccountController : Controller
    {
        private readonly db_shutterContext _context;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(db_shutterContext context, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Validate(UserViewModel model)
        {
            var user = _context.User.Where(u => u.Email == model.Email);
            if (user.Any())
            {
                if (user.Where(u => u.Password == model.Password).Any())
                {
                    HttpContext.Response.Cookies.Append("username", "aadfdsfds");
                    return Json(new { status = true, message = "login successfully" });                                       
                }
                else
                {
                    return Json(new { status = false, message = "valid password" });
                }
            }
            else
            {
                return Json(new { status = false, message = "isvalid email" });
            }
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Signup()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(UserViewModel model)
        {
            //if (ModelState.IsValid)
            //{
            //    var result = await _signInManager.PasswordSignInAsync(
            //        model.Email, model.Password, model.Rememberme, false);


            //    if (result.Succeeded)
            //    {
            //        return RedirectToAction("index", "Home");
            //    }

            //    ModelState.AddModelError(string.Empty, "Invalid Login Attempt");
            //}

            //ViewBag.showtopbar = false;
            //if (ModelState.IsValid) 
            //{ 
            //    var user = new UserViewModel
            //    {
            //        Firstname = model.first_name,
            //        Lastname = model.last_name,
            //        Username = model.username,                   

            //    };

            //    var result = await _userManager.CreateAsync(user, model.password);
            //    if (result.Succeeded)
            //    {
            //        await _signInManager.SignInAsync(user, isPersistent: false);
            //        return RedirectToAction(nameof(HomeController.Index), "Home");
            //    }

            //}

            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult fb_Login(string name, string email)
        {

           HttpContext.Session.SetString("UserName", email);
            return Json(new { success = "True" });

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult fb_Logout()
        {

            HttpContext.Session.Remove("username");
            return Json(new { success = "True" });

        }
    }
}
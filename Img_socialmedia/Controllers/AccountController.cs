using Img_socialmedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System.Linq;
using System.Threading.Tasks;

namespace Img_socialmedia.Controllers
{
    public class AccountController : Controller
    {
        private readonly db_shutterContext _context;

        public AccountController(db_shutterContext context)
        {
            _context = context;

        }

        [HttpPost]
        [AllowAnonymous]
        //[ValidateAntiForgeryToken]
        public IActionResult Validate(string email, string password)
        {
            //return Json(new { status = true, message = "login successfully" });
            var user = _context.User.Where(u => u.Email == email);
            if (user.Any())
            {
                if (user.First().Password == password)
                {
                    string username = user.First().Firstname + " " + user.First().Lastname;
                    HttpContext.Session.SetInt32("userid", user.First().Id);
                    HttpContext.Session.SetString("username", username);

                    return Json(new 
                    { 
                        status = true,
                        message = "login successfully" 
                    });
                }
                else
                {
                    return Json(new 
                    { 
                        status = false, 
                        message = "valid password" 
                    });
                }
            }
            else
            {
                return Json(new 
                { 
                    status = false,
                    message = "isvalid email" 
                });
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

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
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
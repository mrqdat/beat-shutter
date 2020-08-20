using Img_socialmedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
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
            if (user.First() == null)
            {
                return Json(new
                {
                    status = false,
                    message = "Invalid Email"
                });
            }
            if (!user.Any( u => u.hasBlocked ))
            {
                if (user.First().Password == password)
                {
                    string username = user.First().Firstname + " " + user.First().Lastname;
                    HttpContext.Response.Cookies.Append("username", username);
                    HttpContext.Session.SetInt32("userid", user.First().Id);
                    HttpContext.Session.SetString("username", username);
                    HttpContext.Session.SetString("profileimg", user.First().ProfileImg??"/images/man.png");
                    if (user.Any(x=>x.isAdmin))
                    {
                        return Json(new
                        {
                            status = true,
                            name = "admin"
                        });                       
                    }
                    else
                    {
                        return Json(new
                        {
                            status = true,
                            name = "user"
                        });
                    }
                }
                else
                {
                    return Json(new 
                    { 
                        status = false, 
                        message = "invalid password" 
                    });
                }
            }
            else
            {
                return Json(new 
                { 
                    status = false,
                    message = "Your account has been locked. Contact khiet.nguyen2@gmail.com for more information!" 
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
            if (HttpContext.Session.GetInt32("userid").HasValue)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Signup(string firstname, string lastname, string email, string password, string confirmpassword)
        {
            if (string.IsNullOrEmpty(confirmpassword)){
                ModelState.AddModelError("", "Confirm password invalid");
                return View();
            }
            if (!password.Equals(confirmpassword))
            {
                ModelState.AddModelError("", "Password and confirm password don't match");
                return View();
            }
            if(password.Length < 8 || password.Length > 15)
            {
                ModelState.AddModelError("", "Password must have 8 - 15 char");
                return View();
            }
            var user = _context.User.Where(c => c.Email.Equals(email)).FirstOrDefault();
            if (user != null)
            {
                ModelState.AddModelError("", "Email already exists");
                return View();
            }
            var model = new UserViewModel
            {
                Firstname=firstname,
                Lastname=lastname,
                Email=email,
                Password=password,
                CreateAt=DateTime.Now
            };
            if (ModelState.IsValid)
            {
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction("Login", "Account");
            }
            return View();
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
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
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
                    HttpContext.Response.Cookies.Append("username", username);
                    HttpContext.Session.SetInt32("userid", user.First().Id);
                    HttpContext.Session.SetString("username", username);
                    if (username == "admin ")
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
                        }) ;
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
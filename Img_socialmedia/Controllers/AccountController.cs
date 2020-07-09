using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Img_socialmedia.Data;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userdetail = _context.User.SingleOrDefault(m => m.Email == model.email && m.Password == model.password);
                if (userdetail == null)
                {
                    ModelState.AddModelError("password", "invalid ");
                    return View("Index");
                }
                HttpContext.Session.SetString("userid", userdetail.Username);
            }
            else
            {
                return View("Index");
            }
            return Redirect("/");
        }

        public async Task<ActionResult> Signup(RegisterViewModel model)
        {
            ViewBag.showtopbar = false;
            if (ModelState.IsValid)
            {
                UserViewModel user = new UserViewModel()
                {
                    Firstname = model.first_name,
                    Lastname = model.last_name,
                    Username = model.username,
                    Password = model.password,
                    PasswordConfirm = model.confirm_password
                };
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            else
            {
                return View("SignUp");
            }
            return RedirectToAction("Index", "Login");
        }


        public ViewResult Logout()
        {
            HttpContext.Session.Clear();
            return View("Index");
        }
    }
}
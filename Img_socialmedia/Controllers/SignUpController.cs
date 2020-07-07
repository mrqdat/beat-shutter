using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Authorization;

namespace Img_socialmedia.Controllers
{
    public class SignUpController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.showtopbar = false;

            return View();
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public  async Task<IActionResult> signup(RegisterViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        var user = new User (firstname = model.first_name, lastname = model.last_name, username = model.username, email = model.email);
        //        var result = await _userManager.CreateAsync(user, model.password);

        //        if (result.Succeeded)
        //        {
        //            await _signInManager.SignInAsync(user, isPersistent: false);
        //            _logger.LogInfomation(3, "User created ");
        //            return RedirectToAction("Index", "Home");
        //        }

        //        AddErrors(result);
        //    }
        //    return View(model);
        //}

        public IActionResult signup()
        {
            ViewBag.showtopbar = false;

            return View();
        }
    }
}

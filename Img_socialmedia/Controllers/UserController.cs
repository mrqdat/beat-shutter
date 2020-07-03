using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Img_socialmedia.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult editprofile()
        {
            return View();
        }

        public IActionResult password()
        {
            return View();
        }

        public IActionResult email()
        {
            return View();
        }

        public IActionResult close()
        {
            return View();
        }


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    _logger.LogInformation(4, "User logged out");
        //    return RedirectToAction("Index", "Home");
        //}

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;

namespace Img_socialmedia.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.showtopbar = false;

            return View();
        }

        [HttpPost]
        public IActionResult signup(UserViewModel model)
        {
            return View();
        }
    }
}

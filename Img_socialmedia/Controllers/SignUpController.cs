using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Img_socialmedia.Controllers
{
    public class SignUpController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.showtopbar = false;

            return View();
        } 


        public IActionResult signup()
        {
            return View();
        }
    }
}

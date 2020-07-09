using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Authorization;
using Img_socialmedia.Data;

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

        
    }
}

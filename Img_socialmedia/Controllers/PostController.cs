using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Mvc;

namespace Img_socialmedia.Controllers
{
    public class PostController : Controller
    {

        db_shutterContext db = new db_shutterContext();
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AllPost()
        {
            
            return View();
        }

        //public JsonResult getAllPost(string q)
        //{
            
            
        //    return View();
        //}
    }
}

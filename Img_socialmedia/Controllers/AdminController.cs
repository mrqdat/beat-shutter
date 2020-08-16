using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Newtonsoft.Json;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace Img_socialmedia.Controllers
{
    public class AdminController : Controller
    {
        private readonly db_shutterContext _context;
        public AdminController(db_shutterContext context){
            _context = context;
        }
        [HttpGet]
        public IActionResult Index(){
            var user = _context.User.ToList();
            return View(user); 
        }

        public IActionResult report()
        {
            
            var result  = _context.Post.Where(u => u.triggeredBy>0).ToList();
            return View(result);

        }

        [HttpPost]
        public ActionResult doreport(int id)
        {
            if (HttpContext.Session.GetInt32("userid").HasValue)
            {

                var rp = _context.Post.Find(id);
                rp.hasban = true;
                _context.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Your session has expried, please login again to continue working."
                });
            }

        }

        [HttpPost]
        public ActionResult undoreport(int id)
        {
            if (HttpContext.Session.GetInt32("userid").HasValue)
            {

                var rp = _context.Post.Find(id);
                rp.hasban = false;
                _context.SaveChanges();

                return Json(new
                {
                    status = true
                });
            }
            else
            {
                return Json(new
                {
                    status = false,
                    message = "Your session has expried, please login again to continue working."
                });
            }

        }
    }

}

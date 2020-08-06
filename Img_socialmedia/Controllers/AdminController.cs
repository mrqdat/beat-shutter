using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;

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
            return View();
        }
    }

}

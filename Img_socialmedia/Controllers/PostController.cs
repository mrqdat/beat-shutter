using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Server;

namespace Img_socialmedia.Controllers
{
    public class PostController : Controller
    {
        private readonly db_shutterContext _ShutterContext;

        public PostController(db_shutterContext db_Shutter)
        {
            _ShutterContext = db_Shutter;
        }
       
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult getreport()
        {
            
            return View();
        }
       
        public IActionResult report()
        {
            return View();
            
        }
    }
}

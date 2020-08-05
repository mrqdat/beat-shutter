using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace Img_socialmedia.Controllers
{
    public class PostController : Controller
    {

       
        public IActionResult Index()
        {
            return View();
        }

       
    }
}

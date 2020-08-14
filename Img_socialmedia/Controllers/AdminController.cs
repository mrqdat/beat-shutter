using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Newtonsoft.Json;
using System.IO;

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
            
            var result  = _context.Post.Where(u => u.hasban==true).ToList();
            return View(result);
        //    var path = Path.Combine(Directory.GetCurrentDirectory(),$"wwwroot\\{"rp\\report.json"}");
        //     var json = System.IO.File.ReadAllText(path);
            
        //     ReportViewModel rp = JsonConvert.DeserializeObject<ReportViewModel>(json);
        //     using(StreamReader file = System.IO.File.OpenText(path))
        //     {
        //         JsonSerializer serializer = new JsonSerializer();
        //         ReportViewModel reppo = (ReportViewModel) serializer.Deserialize(file,typeof(ReportViewModel));
        //     }

        //     return Json(reppo);
        }

         
    }

}

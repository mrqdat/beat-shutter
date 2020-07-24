using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Img_socialmedia.Controllers
{
    public class PostController : Controller
    {

        private readonly db_shutterContext _context;
        public IActionResult Index()
        {
            return View();
        }

        public ActionResult AllPost()
        {
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> add_collection(CollectionViewModel model)
        {
            var col_name = Request.Form["collection_name"].ToString();
            var col_des = Request.Form["collection_description"].ToString();


            //var username = HttpContext.Session["username"];
            //UserViewModel user = _context.User.FirstOrDefault(x => x.Username == username);

            if(ModelState.IsValid /*|| (username != null)*/)
            {
               // model.UserId = user.Id;
                model.Name = col_name;
                model.CreateAt = DateTime.Now;
                model.Description = col_des;
                _context.Add(model);
                await _context.AddRangeAsync();
            }

            return Ok();
        }


        public async Task<ActionResult> like(PostViewModel model)
        { 
                _context.Add(model);
               await _context.SaveChangesAsync();

            

            return Ok(model);
        }
        public ActionResult dislike(int id)
        {
             
 

            return Ok();
        }
    }
}

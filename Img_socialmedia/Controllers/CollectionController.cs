using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;

namespace Img_socialmedia.Controllers
{
    public class CollectionController : Controller
    {
        private readonly db_shutterContext _context;

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult createCollection(string colname, string coldes, CollectionViewModel model)
        {
            var userid = HttpContext.Session.GetInt32("userid");

            if(userid == null)
            {
                return RedirectToAction("Login", "Account");
            }
            else { 
                if (ModelState.IsValid)
                {
                    model.Name = colname;
                    model.Description = coldes;
                    model.CreateAt = DateTime.Now;
                    model.UserId = userid;
                    _context.Add(model);
                    _context.SaveChanges();
                }
                return Json(new
                {
                    status = true,
                    message = "success"
                });
            }           
        }
    }
}

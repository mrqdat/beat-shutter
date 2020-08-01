using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;

namespace Img_socialmedia.Controllers
{
    public class CollectionController : Controller
    {
        private readonly db_shutterContext _context;

        //public IActionResult createCollection()
        //{
        //    return View();
        //}


        public IActionResult createCollection(string colname, string coldes)
        {
            var userid = HttpContext.Session.GetInt32("userid");
            if (userid != null)
            {
                
                CollectionViewModel model = new CollectionViewModel();
                if (ModelState.IsValid)
                {
                    model.Name = colname;
                    model.Description = coldes;
                    model.CreateAt = DateTime.Now;
                    model.UserId = userid;
                    _context.Add(model);
                    _context.SaveChanges();
                }
                else
                {
                    return Json(new { success = "false", message = "fail" });
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return Json("success");            
        }


        public IActionResult getCollection()
        {
            var userid = HttpContext.Session.GetInt32("userid");
            if(userid != null)
            {
                var result = (from colname in _context.Collection
                              join user in _context.User on colname.UserId equals userid
                              join coldetail in _context.CollectionDetail on colname.Id equals coldetail.CollectionId
                              select colname).ToList();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
            return View();
        }
    }
}

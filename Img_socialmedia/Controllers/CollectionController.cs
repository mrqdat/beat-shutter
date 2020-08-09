using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using System.Dynamic;

namespace Img_socialmedia.Controllers
{
    public class CollectionController : Controller
    {
        private readonly db_shutterContext _context;

        public CollectionController(db_shutterContext context) {
            _context = context;
        }

        //public IActionResult createCollection()
        //{
        //    return View();
        //}


        [HttpPost]
        public IActionResult createCollection(string colname, string coldes)
        {
            var userid = HttpContext.Session.GetInt32("userid");
            if (userid != null)
            {
                // var colname = Request.Form["colname"].ToString();
                // var coldes = Request.Form["coldes"].ToString();
                CollectionViewModel model = new CollectionViewModel();
                CollectionDetailViewModel collectionDetail = new CollectionDetailViewModel();
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
                    return Json(new 
                    { 
                        result = "false",
                        message = "fail" 
                    });
                }
            }
            else
            {
                return Json(new 
                { 
                    result = "Redirect",
                    url = Url.Action("Login", "Account") 
                });
            }
            return Json(new
            {  
                result = "success",
                message = "success"
            });            
        }

       
        [HttpGet]
        public ActionResult addtoCollection()
        {
            var userid = HttpContext.Session.GetInt32("userid");
            CollectionDetailViewModel detailViewModel = new CollectionDetailViewModel();

            return View();
        }        
        
    }
}

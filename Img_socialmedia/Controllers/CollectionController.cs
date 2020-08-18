using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.ObjectModel;
using System.Dynamic;
using Microsoft.EntityFrameworkCore;

namespace Img_socialmedia.Controllers
{
    public class CollectionController : Controller
    {
        private readonly db_shutterContext _context;

        public CollectionController(db_shutterContext context)
        {
            _context = context;
        }

        //public IActionResult createCollection()
        //{
        //    return View();
        //}

        [HttpGet]
        public IActionResult GetCollection()
        {
            if (HttpContext.Session.GetInt32("userid").HasValue)
            {
                var userid = HttpContext.Session.GetInt32("userid").Value;
                var model = _context.Collection.Where(d => d.UserId == userid).ToList();
                return Json(model);
            }
            return Json(new { status = false });
        }

        [HttpPost]
        public IActionResult createCollection(string colname, string coldes)
        {
            var userid = HttpContext.Session.GetInt32("userid").Value;
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
                    return Json(new
                    {
                        result = "success",
                        message = model
                    });
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
            //return Json(new
            //{  
            //    result = "success",
            //    message = "success"
            //});            
        }


        [HttpGet]
        public ActionResult AddtoCollection(int collectionid, int postid)
        {
            try
            {
                if (!(collectionid > 0) || !(postid > 0))
                {
                    return View("Error");
                }
            }
            catch
            {
                return View("Error");
            }

            var collection = _context.Collection.Find(collectionid);
            var post = _context.Post.Where(p => p.hasban == false)
                                    .Where(p => p.Id == postid)
                                    .First();

            if (post == null || collection == null)
            {
                return View("Error");
            }

            //var collectionIsExist = _context.CollectionDetail.Where(p=>p.PostId==postid).First();
            //if(collectionIsExist != null)
            //{
            //    return Json(new { status = false });
            //}


            if (HttpContext.Session.GetInt32("userid").HasValue)
            {
                var model = new CollectionDetailViewModel
                {
                    PostId = postid,
                    CollectionId = collectionid,
                };
                _context.Add(model);
                _context.SaveChanges();
                var url = Url.Action("Index", "Post", new { id = postid });
                return Redirect(url);
            }

            return View("Error");
        }        
        public IActionResult GetCollectionDetail(int id)
        {
            try
            {
                if (id  >0)
                {
                    var model = from cd in _context.CollectionDetail
                                join a in _context.Post on cd.PostId equals a.Id
                                join p in _context.Photo on a.PhotoId equals p.Id
                                where cd.CollectionId == id
                                select new CollectionDetailViewModel
                                {
                                    Id = cd.Id,
                                    PostId = cd.PostId,
                                    CollectionId = cd.CollectionId,
                                    PostPhoto = p.Url
                                };
                    return Json(model.ToList());
                }
            }
            catch
            {
                return Json(new { status = false });
            }
            return Json(new { status=false});
        }
    }
}

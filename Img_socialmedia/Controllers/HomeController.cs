using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Img_socialmedia.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Globalization;

namespace Img_socialmedia.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly db_shutterContext shutterContext;

        public HomeController(IConfiguration config,db_shutterContext context)
        {
            this.configuration = config;
            shutterContext = context;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var result = from photo in shutterContext.Photo
                         join post in shutterContext.Post on photo.Id equals post.PhotoId
                         join user in shutterContext.User on post.UserId equals user.Id
                         select new PPUModel()
                         {
                             photoP = photo,
                             postP = post,
                             userP = user
                         };
                        
            return View(result.ToList());
        }

        public ViewResult Privacy()
        {
            return View();
        }
        public ViewResult Upload()
        {
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       public ViewResult About()
        {
            return View();
        }

       public IEnumerable<PostViewModel> TestA()
        {
            var result = from photo in shutterContext.Photo
                         join post in shutterContext.Post on photo.Id equals post.PhotoId
                         join user in shutterContext.User on post.UserId equals user.Id
                         select new PostViewModel
                         {
                             Id = post.Id,
                             PhotoId = photo.Id,
                             TotalLike = post.TotalLike,
                             TotalViews = post.TotalViews,
                             CreateAt = post.CreateAt,
                             Tags = post.Tags,
                             UserId = user.Id,
                             User = user,
                             Photo = photo,
                         };

            return result.ToList();               
        }
       
        [HttpGet]
       public PostViewModel GetDetailPhoto(int? id)
       {
            //var result = from photo in shutterContext.Photo
            //             join post in shutterContext.Post on photo.Id equals post.PhotoId
            //             join user in shutterContext.User on post.UserId equals user.Id
            //             join comment in shutterContext.Comment on post.Id equals comment.PostId into commentGroup
            //             where post.Id == id
            //             select new PostViewModel
            //             {
            //                 Id = post.Id,
            //                 PhotoId = photo.Id,
            //                 TotalLike = post.TotalLike,
            //                 TotalViews = post.TotalViews,
            //                 CreateAt = post.CreateAt,
            //                 Tags = post.Tags,
            //                 UserId = user.Id,
            //                 User = user,
            //                 Photo = photo,
            //                 Comment = commentGroup,
            //             };

           

            return result.First();
        }
    }
}

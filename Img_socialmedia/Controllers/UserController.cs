using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Img_socialmedia.Data;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Microsoft.Extensions.FileProviders;

namespace Img_socialmedia.Controllers
{
    public class UserController : Controller
    {
        private readonly db_shutterContext _context;
        private IWebHostEnvironment environment;
        public UserController(db_shutterContext context, IWebHostEnvironment environment)
        {
            this._context = context;
            this.environment = environment;
        }

        public IActionResult Index()
        {       
            return View();
        }

        // GET: Users/Details/5
        public  IActionResult Details(int id)
        {
                if (id.ToString() == null)
                {
                    return View("Error");;
                }

                if (HttpContext.Session.GetInt32("userid").HasValue)
                {
              
                        var us = _context.User
                        .Include(p => p.Post)
                            .ThenInclude(photo => photo.Photo)
                        .Include(d => d.Collection)
                            .ThenInclude(d => d.CollectionDetail)
                        .Where(d => d.Id == id).First();

                        var follow = _context.Follow.Where(d =>
                                                            d.UserId == HttpContext.Session.GetInt32("userid").Value &&
                                                            d.FollowingUserId == us.Id).FirstOrDefault();

                    var follower = _context.Follow.Where(d => d.UserId == id).Count();
                    var following = _context.Follow.Where(d => d.FollowingUserId == id).Count();
                ViewData["Following"] = following;
                ViewData["Follower"] = follower;
                    if (follow != null)
                        {
                            us.hasFollowed = true;
                        }
                        else
                        {
                            us.hasFollowed = false;
                        }
                        if (us == null)
                        {
                            return View("Error"); ;
                        }

                        return View("Index", us);
                }
                var user = _context.User
                    .Include(p => p.Post)
                        .ThenInclude(photo => photo.Photo)
                    .Include(d => d.Collection)
                        .ThenInclude(d => d.CollectionDetail)
                    .Where(d => d.Id == id).First();

                if (user == null)
                {
                    return View("Error");;
                }
            var follower1 = _context.Follow.Where(d => d.UserId == id).Count();
            var following1 = _context.Follow.Where(d => d.FollowingUserId == id).Count();
            ViewData["Following"] = following1;
            ViewData["Follower"] = follower1;

            return View("Index", user);
        }
       
        [HttpGet]
        public IActionResult password()
        {
            if (!HttpContext.Session.GetInt32("userid").HasValue)
            {
                return View("Error");
            }
            var user = _context.User.Find(HttpContext.Session.GetInt32("userid").Value);
            return View(user);
        }

        [HttpPost]
        public IActionResult password(string Password, string NewPassword, string ConfirmPassword)
        {
            if (!HttpContext.Session.GetInt32("userid").HasValue)
            {
                return View("Error");
            }

            var user = _context.User.Find(HttpContext.Session.GetInt32("userid").Value);

            if (string.IsNullOrEmpty(Password) || string.IsNullOrEmpty(NewPassword) || string.IsNullOrEmpty(ConfirmPassword))
            {
                ModelState.AddModelError("", "Password, new password, confirm password must not null or empty");
                return View(user);
            }

            if (!user.Password.Equals(Password))
            {
                ModelState.AddModelError("", "Password invalid");
                return View(user);
            }

            if(NewPassword.Length<8 || NewPassword.Length > 15)
            {
                ModelState.AddModelError("", "New password must have 8 - 15 char");
                return View(user);
            }

            if (NewPassword.Equals(ConfirmPassword))
            {
                user.Password = NewPassword;
                _context.SaveChanges();
                return View("editprofile",user);
            }

            ModelState.AddModelError("", "New password & confirm password don't match");
            return View(user);
        }
        [HttpGet]
        public IActionResult email()
        {
            if (!HttpContext.Session.GetInt32("userid").HasValue)
            {
                return View("Error");
            }
            var user = _context.User.Find(HttpContext.Session.GetInt32("userid").Value);
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> email(string email)
        {
            if (!HttpContext.Session.GetInt32("userid").HasValue)
            {
                return View("Error");
            }
            var user = _context.User.Find(HttpContext.Session.GetInt32("userid").Value);
            user.Email = email;
            await _context.SaveChangesAsync();
            return RedirectToAction("Details", new { id = user.Id });
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,username,password,PasswordConfirm,email,name,first_name,last_name,phone,create_at")] UserViewModel userViewModel)
        {
            if (ModelState.IsValid)
            {
                
                _context.Add(userViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        [HttpGet]
        public async Task<IActionResult> editprofile(int id)
        {
            int sessionID;
            try
            {
                sessionID = (int)HttpContext.Session.GetInt32("userid");
            }
            catch
            {
                return View("Error");
            }
            if (!String.IsNullOrEmpty(sessionID.ToString()))
            {
                if (sessionID == id)
                {
                    if (id.ToString() == null)
                    {
                        return View("Error");
                    }

                    var user = await _context.User
                        .FirstOrDefaultAsync(m => m.Id == id);
                    var model = new EditUserViewModel
                    {
                        ProfileImg = user.ProfileImg,
                        Firstname=user.Firstname,
                        Lastname=user.Lastname,
                        Bio=user.Bio,
                        Email=user.Email,
                        Phone=user.Phone,
                        Tags=user.Tags,
                        CreateAt=user.CreateAt
                    };
                    if (user == null)
                    {
                        return View("Error");;
                    }
                    return View("editprofile", model);
                }
                else
                {
                    return View("Error");;
                }
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> editprofile(EditUserViewModel model)
        {
            int userId;
            try
            {
                userId = HttpContext.Session.GetInt32("userid").Value;
            }
            catch
            {
                return View("Error");
            }
            var user = _context.User.Find(userId);
            if (!user.Email.Equals(model.Email))
            {
                return View("Error");
            };
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var files = HttpContext.Request.Form.Files;
                foreach (var file in files)
                {
                    var extensions = Path.GetExtension(file.FileName);
                    if (extensions.Contains(".jpg") || extensions.Contains(".png"))
                    {
                        var filename = Path.GetFileName(file.FileName);
                        var myUniqueFileName = Guid.NewGuid() + "_" + HttpContext.Session.GetInt32("userid");
                        var fileExtension = Path.GetExtension(filename);
                        var newFileName = String.Concat(myUniqueFileName, fileExtension);
                        var filePath = new PhysicalFileProvider(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "images")).Root + $@"\{ newFileName}";
                        // full path to file in temp location
                        //var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await file.CopyToAsync(stream);
                        }
                        user.ProfileImg = "\\images\\" + newFileName;
                    }
                }

            }

            user.Bio = model.Bio;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Phone = model.Phone;
            user.Tags = model.Tags;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = user.Id });
        }
        /// <summary>
        /// Check user login 
        /// </summary>
        /// <returns>true - false</returns>
        public bool IsUserLogin()
        {
            if(HttpContext.Session.GetInt32("userid").HasValue)
            {
                return true; 
            }
            return false;
        }
        [HttpPost]
        public IActionResult Follow(int id)
        {
            if (!IsUserLogin())
            {
                return Json(new { status = false });
            }
            var user = _context.User.Find(id);
            if (user == null)
            {
                return Json(new { status = false });
            }
            var userid = HttpContext.Session.GetInt32("userid").Value;
            var follow = _context.Follow.FirstOrDefault(d => d.UserId == userid && d.FollowingUserId == id);
            if (follow != null)
            {
                _context.Follow.Remove(follow);
                _context.SaveChanges();
                return Json(new { status = true,msg=1 });
            }
            var model = new FollowViewModel
            {
                UserId = userid,
                FollowingUserId = id
            };
            _context.Follow.Add(model);
            _context.SaveChanges();
            return Json(new { status = true,msg=2 });
        }

        [HttpGet]
        public IActionResult Follower(int id)
        {
           // int userid = HttpContext.Session.GetInt32("userid").Value;

            List<UserViewModel> userList = new List<UserViewModel>();

            var follow = _context.Follow.Where(d => d.UserId == id).ToList();


            foreach (var item in follow)
            {
                var result = _context.User
                                            .Where(d => d.Id == item.FollowingUserId)
                                            .Select(d => new UserViewModel
                                            {
                                                Id = d.Id,
                                                Firstname = d.Firstname,
                                                Lastname = d.Lastname,
                                                ProfileImg = d.ProfileImg
                                            }).First();
                userList.Add(result);
            }
            return Ok(userList);
        }

        [HttpGet]
        public IActionResult Following(int id)
        {

            //int userid = HttpContext.Session.GetInt32("userid").Value;

            List<UserViewModel> userList = new List<UserViewModel>();

            var follow = _context.Follow.Where(d => d.FollowingUserId == id).ToList();


            foreach (var item in follow)
            {
                var result = _context.User
                                            .Where(d => d.Id == item.UserId)
                                            .Select(d => new UserViewModel
                                            {
                                                Id = d.Id,
                                                Firstname = d.Firstname,
                                                Lastname = d.Lastname,
                                                ProfileImg = d.ProfileImg
                                            }).First();
                userList.Add(result);
            }
            return Ok(userList);
        }
    }
}

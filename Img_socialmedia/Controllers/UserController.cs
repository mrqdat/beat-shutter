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

namespace Img_socialmedia.Controllers
{
    public class UserController : Controller
    {
        private readonly db_shutterContext _context;
     
        public UserController(db_shutterContext context)
        {
            this._context = context;
        }

        public IActionResult Index()
        {       
            return View();
        }

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id.ToString() == null)
            {
                return View("Error");;
            }

            var user = await _context.User
                .Include(p=>p.Post)
                    .ThenInclude(photo=>photo.Photo)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return View("Error");;
            }

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

        public IActionResult email()
        {
            if (!HttpContext.Session.GetInt32("userid").HasValue)
            {
                return View("Error");
            }
            var user = _context.User.Find(HttpContext.Session.GetInt32("userid").Value);
            return View(user);
        }

        public IActionResult close()
        {
            if (!HttpContext.Session.GetInt32("userid").HasValue)
            {
                return View("Error");
            }
            var user = _context.User.Find(HttpContext.Session.GetInt32("userid").Value);
            return View(user);
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

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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
            }

            user.Bio = model.Bio;
            user.Firstname = model.Firstname;
            user.Lastname = model.Lastname;
            user.Phone = model.Phone;
            user.Tags = model.Tags;

            await _context.SaveChangesAsync();

            return RedirectToAction("Details", new { id = user.Id });
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id.ToString() == null)
            {
                return View("Error");;
            }

            var userViewModel = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userViewModel == null)
            {
                return View("Error");;
            }

            return View(userViewModel);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userViewModel = await _context.User.FindAsync(id);
            _context.User.Remove(userViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}

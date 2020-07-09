using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Img_socialmedia.Data;
using Img_socialmedia.Models;

namespace Img_socialmedia.Controllers
{
    public class UserController : Controller
    {
        private readonly db_shutterContext _context;

        public UserController(db_shutterContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        // GET: Users
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.User.ToListAsync());
        //}

        // GET: Users/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userViewModel == null)
            {
                return NotFound();
            }

            return View(userViewModel);
        }

        public IActionResult editprofile()
        {
            return View();
        }

        public IActionResult password()
        {
            return View();
        }

        public IActionResult email()
        {
            return View();
        }

        public IActionResult close()
        {
            return View();
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.User.FindAsync(id);
            if (userViewModel == null)
            {
                return NotFound();
            }
            return View(userViewModel);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,username,password,PasswordConfirm,email,name,first_name,last_name,phone,create_at")] UserViewModel userViewModel)
        {
            if (id != userViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(userViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(userViewModel);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var userViewModel = await _context.User
                .FirstOrDefaultAsync(m => m.Id == id);
            if (userViewModel == null)
            {
                return NotFound();
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


        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    _logger.LogInformation(4, "User logged out");
        //    return RedirectToAction("Index", "Home");
        //}
    }
}

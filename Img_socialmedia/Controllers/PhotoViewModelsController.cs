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
    public class PhotoViewModelsController : Controller
    {
        private readonly Img_socialmediaContext _context;

        public PhotoViewModelsController(Img_socialmediaContext context)
        {
            _context = context;
        }

        // GET: PhotoViewModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.PhotoViewModel.ToListAsync());
        }

        // GET: PhotoViewModels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoViewModel = await _context.PhotoViewModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (photoViewModel == null)
            {
                return NotFound();
            }

            return View(photoViewModel);
        }

        // GET: PhotoViewModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PhotoViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,url,camera_model,aperture,shutter_speed,focal_length,ISO,width,height,location,color_code,create_at")] PhotoViewModel photoViewModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(photoViewModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(photoViewModel);
        }

        // GET: PhotoViewModels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoViewModel = await _context.PhotoViewModel.FindAsync(id);
            if (photoViewModel == null)
            {
                return NotFound();
            }
            return View(photoViewModel);
        }

        // POST: PhotoViewModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("id,url,camera_model,aperture,shutter_speed,focal_length,ISO,width,height,location,color_code,create_at")] PhotoViewModel photoViewModel)
        {
            if (id != photoViewModel.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(photoViewModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PhotoViewModelExists(photoViewModel.id))
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
            return View(photoViewModel);
        }

        // GET: PhotoViewModels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var photoViewModel = await _context.PhotoViewModel
                .FirstOrDefaultAsync(m => m.id == id);
            if (photoViewModel == null)
            {
                return NotFound();
            }

            return View(photoViewModel);
        }

        // POST: PhotoViewModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var photoViewModel = await _context.PhotoViewModel.FindAsync(id);
            _context.PhotoViewModel.Remove(photoViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoViewModelExists(string id)
        {
            return _context.PhotoViewModel.Any(e => e.id == id);
        }
    }
}

﻿using Img_socialmedia.Data;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Img_socialmedia.Controllers
{
    public class PhotosController : Controller
    {
        private readonly db_shutterContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public PhotosController(db_shutterContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            webHostEnvironment = hostEnvironment; 
        }
        // GET: PhotoViewModels
        public IActionResult  Index()
        {
            return View();
        }

        // GET: PhotoViewModels/Details/5
        public async Task<IActionResult> Details(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var photoViewModel = await _context.Photo 
                .FirstOrDefaultAsync(m => m.Id == id);
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
        public async Task<IActionResult> Create([Bind("id,url,camera_model,aperture,shutter_speed,focal_length,ISO,width,height,location,create_at")] PhotoViewModel photoViewModel)
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

            var photoViewModel = await _context.Photo.FindAsync(id);
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
        public async Task<IActionResult> Edit(int id, [Bind("id,url,camera_model,aperture,shutter_speed,focal_length,ISO,width,height,location,create_at")] PhotoViewModel photoViewModel)
        {
            if (id != photoViewModel.Id)
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
                    if (!PhotoViewModelExists(photoViewModel.Id))
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
        public async Task<IActionResult> Delete(int id)
        {
            if (id.ToString() == null)
            {
                return NotFound();
            }

            var photoViewModel = await _context.Photo
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var photoViewModel = await _context.Photo.FindAsync(id);
            _context.Photo.Remove(photoViewModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PhotoViewModelExists(int id)
        {
            return _context.Photo.Any(e => e.Id == id);
        }

      
        public async Task<IActionResult> photoUpload(List<IFormFile> files)
        {

            //var newFileName = string.Empty;

            //if (HttpContext.Request.Form.Files != null)
            //{
            //    var fileName = string.Empty;
            //    string PathDB = string.Empty;

            //    var files = HttpContext.Request.Form.Files;

            //    foreach (var file in files)
            //    {
            //        if (file.Length > 0)
            //        {

            //            fileName = ContentDispositionHeaderValue
            //                    .Parse(file.ContentDisposition)
            //                    .FileName
            //                    .Trim('"');

            //            var myUniqueFileName = Convert.ToString(Guid.NewGuid());

            //            var FileExtension = Path.GetExtension(fileName);

            //            newFileName = myUniqueFileName + FileExtension;

            //            fileName = Path.Combine(webHostEnvironment.WebRootPath, "images") + $@"\{newFileName}";

            //            PathDB = newFileName;

            //            using (FileStream fs = System.IO.File.Create(fileName))
            //            {
            //                file.CopyTo(fs);
            //                fs.Flush();
            //            }
            //        }
            //    }


            //}
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            return Ok(new { count = files.Count, size, filePaths });
        }

        public ActionResult reading_metadata()
        {

            string file = @"C:\Users\QDat\source\repos\Img_sharing\Img_socialmedia\wwwroot\images\hoa.png";

            FileInfo info = new FileInfo(file);
            long filesize = info.Length;

            FileStream fileStream = new FileStream(file, FileMode.Open, FileAccess.Read);
            Image image = Image.FromStream(fileStream, false, false);

            int width = image.Width;
            int height = image.Height;

            try
            {
                PropertyItem item;
                string data;
                string model;
                string version;


                DateTime datetaken;

                item = image.GetPropertyItem(0x9000);
                version = Encoding.UTF8.GetString(item.Value);

                item = image.GetPropertyItem(0x0110);
                model = Encoding.UTF8.GetString(item.Value, 0, item.Value.Length - 1);

                item = image.GetPropertyItem(0x9003);
                data = Encoding.UTF8.GetString(item.Value, 0, item.Value.Length - 1);
                datetaken = DateTime.ParseExact(data, "yyyy:MM:dd HH:mm:ss", CultureInfo.InvariantCulture);


                //item = image.GetPropertyItem(0x011A);
                //width = Encoding.UTF8.GetString(item.Value,);
            }
            catch
            {

            }

            return View();
        }
    }
}

using Img_socialmedia.Data;
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
using MetadataExtractor;
using MetadataExtractor.Formats.Exif;
using Microsoft.Extensions.FileProviders;
using System.Reflection.Metadata;
using MetadataExtractor.Formats.Jpeg;

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
        public IActionResult Index()
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
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: PhotoViewModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(string img_url, PhotoViewModel model)
        //{
        //    if (ModelState.IsValid)
        //    {
                 

        //        _context.Add(model);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(model);
        //}

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


        public ActionResult read_metadata(string filepath)
        {

            var directories = ImageMetadataReader.ReadMetadata(filepath);

            foreach (var dir in directories)
            {

            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> files, PhotoViewModel model)
        {
            long size = files.Sum(f => f.Length);


            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    var filename = Path.GetFileName(formFile.FileName);

                    var myUniqueFileName = Convert.ToString(Guid.NewGuid())+ " - shutter";

                    var fileExtension = Path.GetExtension(filename);

                    var newFileName = String.Concat(myUniqueFileName, fileExtension);

                    var filePath = new PhysicalFileProvider(Path.Combine(System.IO.Directory.GetCurrentDirectory(), "wwwroot", "images")).Root + $@"\{ newFileName}";
                    // full path to file in temp location
                    //var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }

                    //var directories = ImageMetadataReader.ReadMetadata(filePath);
                    //model.Url = newFileName;
                    ////model.CameraModel = JpegMetadataReader.ReadMetadata().;
                    //model.Aperture = Convert.ToString(ExifDirectoryBase.TagAperture);
                    ////model.Iso = directories;
                    //model.ShutterSpeed = Convert.ToString(ExifDirectoryBase.TagShutterSpeed);
                    //model.FocalLength = ExifDirectoryBase.TagFocalLength;
                    //model.Location = Convert.ToString(ExifDirectoryBase.TagSubjectLocation);
                    //model.CreateAt = Convert.ToDateTime(ExifDirectoryBase.TagDateTimeOriginal);
                    return RedirectToAction("Index", "Home");
                }
                
            }

            // process uploaded files
            // Don't rely on or trust the FileName property without validation.

            // return Ok(new { count = files.Count, size, filePaths });
            return View();
        }
    }
}
// preate proc 
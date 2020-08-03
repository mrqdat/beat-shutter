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
using System.Runtime.InteropServices.ComTypes;

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
        public async Task<IActionResult> Create(List<IFormFile> files)
        {
            if (files.Count != 0)
            {
                long size = files.Sum(f => f.Length);
                var filePaths = new List<string>();
                foreach (var formFile in files)
                {
                    if (formFile.Length > 0)
                    {
                        var filename = Path.GetFileName(formFile.FileName);
                        var myUniqueFileName = Convert.ToString(Guid.NewGuid()) + " - shutter";
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
                        Image image = Image.FromFile(filePath);
                        //var directories = Encoding.UTF8.GetString(image.GetPropertyItem(0x010F).Value);
                        //var focal = Encoding.UTF8.GetString(image.GetPropertyItem(0x920A).Value);
                        //var equip = Encoding.UTF8.GetString(image.GetPropertyItem(0x0110).Value);
                        //var aperture = Encoding.UTF8.GetString(image.GetPropertyItem(0x9202).Value);
                        //var height = Encoding.UTF8.GetString(image.GetPropertyItem(0x0101).Value);
                        //var width = Encoding.UTF8.GetString(image.GetPropertyItem(0x0100).Value);

                        string focal = "";
                        int ISO = 0;
                        int height = 0;
                        int width = 0;
                        string camera_model = "";
                        string aperture = "";
                        string shutter = "";
                        var directories = ImageMetadataReader.ReadMetadata(filePath);
                        foreach (var directory in directories)
                        {
                            foreach (var tag in directory.Tags)
                            {
                                if (tag.Name == "Focal Length")
                                {
                                    focal = tag.Description.Replace(" mm", "");
                                }
                                if (tag.Name == "ISO Speed Ratings")
                                {
                                    ISO = Convert.ToInt32(tag.Description);
                                }
                                if (tag.Name == "Image Height")
                                {
                                    height = Convert.ToInt32(tag.Description.Replace(" pixels", ""));
                                }
                                if (tag.Name.Equals("Image Width"))
                                {
                                    width = Convert.ToInt32(tag.Description.Replace(" pixels", ""));
                                }
                                if (tag.Name.Equals("Make"))
                                {
                                    camera_model = tag.Description;
                                }
                                if (tag.Name.Equals("Aperture Value"))
                                {
                                    aperture = tag.Description;
                                }
                                if (tag.Name.Equals("Shutter Speed Value"))
                                {
                                    shutter = tag.Description.Replace(" sec", "");
                                }
                            }
                        }

                        PhotoViewModel photo = new PhotoViewModel
                        {
                            Url = "\\images\\"+ newFileName,
                            Height = height,
                            Width = width,
                            CameraModel = camera_model,
                            Aperture = aperture,
                            ShutterSpeed = shutter,
                            FocalLength=focal,
                            Location = HttpContext.Request.Form["Location"],
                            Iso = ISO,
                            CreateAt = DateTime.Now
                        };
                        try
                        {
                            _context.Photo.Add(photo);
                            await _context.SaveChangesAsync();
                        }
                        catch(Exception ex)
                        {
                            return Ok(ex.Message);
                        }
                      
                        PostViewModel post = new PostViewModel
                        {
                            PhotoId = photo.Id,
                            TotalLike = 0,
                            TotalViews = 0,
                            CreateAt = DateTime.Now,
                            UserId = Convert.ToInt32(HttpContext.Session.GetInt32("userid")),
                            Tags = HttpContext.Request.Form["tags"]
                        };
                        try
                        {
                            _context.Post.Add(post);
                            await _context.SaveChangesAsync();
                        }
                        catch(Exception e)
                        {
                            return Ok(e);
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

    }
}
// preate proc 
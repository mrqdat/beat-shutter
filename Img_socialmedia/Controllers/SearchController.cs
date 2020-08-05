using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Img_socialmedia.Controllers
{
    
    public class SearchController : Controller
    {
        private readonly db_shutterContext _context;

        public SearchController(db_shutterContext context){
            _context = context;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var data = from m in _context.Post select m;
            if (string.IsNullOrEmpty(searchString))
            {
                data = data.Where(m => m.Tags.Contains(searchString));
            }
            return View(await data.ToArrayAsync());
        }
    }
}

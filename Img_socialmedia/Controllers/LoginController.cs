using Img_socialmedia.Data;
using Img_socialmedia.Models;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace Img_socialmedia.Controllers
{
    public class LoginController : Controller
    {
        private readonly db_shutterContext _context;

        public LoginController(db_shutterContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.showtopbar = false;
            return View();
        }

        


    }
}
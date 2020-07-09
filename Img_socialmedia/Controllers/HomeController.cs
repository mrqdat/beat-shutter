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

namespace Img_socialmedia.Controllers
{
    
    public class HomeController : Controller
    {

        private readonly IConfiguration configuration;
        private readonly ILogger<HomeController> _logger;

        public HomeController(IConfiguration config)
        {
            this.configuration = config;
        }
        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            //string connectionstring = configuration.GetConnectionString("DefaultConnection");

            //SqlConnection connection = new SqlConnection(connectionstring);
            //connection.Open();
            //SqlCommand cmd = new SqlCommand("select * from photo", connection);
            //cmd.ExecuteScalar();
            //connection.Close();
            return View();
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

       
    }
}

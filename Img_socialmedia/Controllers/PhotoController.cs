using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Img_socialmedia.Controllers
{
    public class PhotoController : Controller
    {
        public IActionResult Index()
        {
            return View();
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
                model = Encoding.UTF8.GetString(item.Value,0,  item.Value.Length - 1);

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

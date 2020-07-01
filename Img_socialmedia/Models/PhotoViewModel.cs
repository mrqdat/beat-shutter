using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Img_socialmedia.Models
{
    public class PhotoViewModel
    {
        [Key]
        public string id { get; set; }
        [Url]
        public string url { get; set; }
        public string camera_model { get; set; }
        public string aperture { get; set; }
        public string shutter_speed { get; set; }
        public string focal_length { get; set; }
        public int ISO { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public string location { get; set; }
        public int color_code { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime create_at { get; set; }
    }
}

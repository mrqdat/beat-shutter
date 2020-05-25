using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Img_socialmedia.Models
{
    public class UserViewModel
    {
        public string id { get; set; }
        public string username { get; set; }
        public string name { get; set; }
        public string frist_name { get; set; }
        public string last_name { get; set; }
        public string bio { get; set; }
        public string location { get; set; }
        public string instagram_username { get; set; }
        public string twitter_username { get; set; }
        public int total_like { get; set; }
        public int total_photos { get; set; }
        public int total_collections { get; set; }
        public int follower_count { get; set; }
        public int following_count { get; set; }
        public int downloads { get; set; }
        [DataType(DataType.Date)]
        public DateTime update_at { get; set; }
    }
}

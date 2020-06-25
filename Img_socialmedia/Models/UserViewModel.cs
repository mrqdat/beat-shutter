using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;

namespace Img_socialmedia.Models
{
    public class UserViewModel
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [DataType(DataType.Password), Compare(nameof(password))]
        public string confirm_password { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string token_id { get; set; }
        public string profile_img { get; set; }
        public string bio { get; set; }
        [DataType(DataType.Date)]
        public DateTime create_at { get; set; }
    }
}

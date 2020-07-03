using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;

namespace Img_socialmedia.Models
{
    public class UserViewModel:IdentityUser
    {
        [Key]
        public string id { get; set; }
        [Required]
        public string username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string password { get; set; }

        [DataType(DataType.Password), Compare(nameof(password))]
        public string confirm_password { get; set; }
        public string email { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string phone { get; set; }

        [DataType(DataType.Date)]
        public DateTime create_at { get; set; }
    }
}

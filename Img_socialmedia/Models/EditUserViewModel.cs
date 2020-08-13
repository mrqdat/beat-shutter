using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Img_socialmedia.Models
{
    public class EditUserViewModel
    {

        [StringLength(100)]
        public string Firstname { get; set; }
        [StringLength(100)]
        public string Lastname { get; set; }

        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "The email field is not valid email address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [StringLength(12)]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Phone include only number, max length 12")]
        public string Phone { get; set; }
        public string ProfileImg { get; set; }
        [StringLength(1000)]
        public string Bio { get; set; }
        public string Tags { get; set; }
        public DateTime CreateAt { get; set; }
    }
}

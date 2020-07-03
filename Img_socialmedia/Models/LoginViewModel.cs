using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Img_socialmedia.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "The email field is not valid email address.")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string password { get; set; }


    }
}

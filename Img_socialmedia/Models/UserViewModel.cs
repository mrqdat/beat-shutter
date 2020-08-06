using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Img_socialmedia.Models
{
    public class UserViewModel
    {
        public UserViewModel()
        {
            Collection = new HashSet<CollectionViewModel>();
            Comment = new HashSet<CommentViewModel>();
            ExternalLogin = new HashSet<ExternalLoginViewModel>();
            Notification = new HashSet<NotificationViewModel>();
            //string Username = Firstname + Lastname;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        //[DataType(DataType.Password)]
 
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Password confirm does not match.")]
        //public string PasswordConfirm { get; set; }

        [StringLength(100)]
        public string Firstname { get; set; }
        [StringLength(100)]
        public string Lastname { get; set; }
        
        [Required(ErrorMessage = "The email field is required.")]
        [EmailAddress(ErrorMessage = "The email field is not valid email address")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}")]
        public string Email { get; set; }
        [StringLength(12)]
        [RegularExpression("^[0-9]*$",ErrorMessage = "Phone include only number, max length 12")]
        public string Phone { get; set; }
        public string TokenId { get; set; }
        public string ProfileImg { get; set; }
        [StringLength(1000)]
        public string Bio { get; set; }        

        [DataType(DataType.Date)]
        public DateTime? CreateAt { get; set; }
        public int? FollowerCount { get; set; }
        public string Tags { get; set; }
        public virtual IEnumerable<PostViewModel> Post { get; set; }
        public virtual ICollection<CollectionViewModel> Collection { get; set; }
        public virtual ICollection<CommentViewModel> Comment { get; set; }
        public virtual ICollection<ExternalLoginViewModel> ExternalLogin { get; set; }
        public virtual ICollection<NotificationViewModel> Notification { get; set; }
    }
}

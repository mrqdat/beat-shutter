using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            FullName = Firstname + " " + Lastname;
        }
        [Key]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$")]
        public string Password { get; set; }

        [DataType(DataType.Password), Compare(nameof(Password))]
        public string PasswordConfirm { get; set; }

       
        public string Firstname { get; set; }
        [StringLength(100)]
        public string Lastname { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TokenId { get; set; }
        public string ProfileImg { get; set; }
        public string Bio { get; set; }

        [DataType(DataType.Date)]
        public DateTime? CreateAt { get; set; }
        public int? FollowerCount { get; set; }
        public string Tags { get; set; }

        public virtual ICollection<CollectionViewModel> Collection { get; set; }
        public virtual ICollection<CommentViewModel> Comment { get; set; }
        public virtual ICollection<ExternalLoginViewModel> ExternalLogin { get; set; }
        public virtual ICollection<NotificationViewModel> Notification { get; set; }
    }
}

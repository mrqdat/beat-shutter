using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Img_socialmedia.Models
{
    public partial class PostViewModel
    {
        public PostViewModel()
        {
            Comment = new HashSet<CommentViewModel>();
            Notification = new HashSet<NotificationViewModel>();
        }

        public int Id { get; set; }
        public int PhotoId { get; set; }
        public int TotalLike { get; set; }
        public int TotalViews { get; set; }
        public DateTime CreateAt { get; set; }
        public string Tags { get; set; }
        public int UserId { get; set; }
        [NotMapped]
        public string Username { get; set; }
        [NotMapped]
        public string UserImg { get; set; }
        public virtual UserViewModel User { get; set; }
        public virtual PhotoViewModel Photo { get; set; }
        public virtual IEnumerable<CommentViewModel> Comment { get; set; }
        public virtual IEnumerable<NotificationViewModel> Notification { get; set; }
        [NotMapped]
        public virtual IEnumerable<PostViewModel> RelatedPost { get; set; }
    }
}

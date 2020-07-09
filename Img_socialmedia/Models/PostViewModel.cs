using System;
using System.Collections.Generic;

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
        public int? TotalLike { get; set; }
        public int? TotalViews { get; set; }
        public DateTime? CreateAt { get; set; }
        public string Tags { get; set; }

        public virtual PhotoViewModel Photo { get; set; }
        public virtual ICollection<CommentViewModel> Comment { get; set; }
        public virtual ICollection<NotificationViewModel> Notification { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class CommentViewModel
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime? CreateAt { get; set; }
        public virtual string UserImg { get; set; }
        public virtual string Username { get; set; }
        public virtual PostViewModel Post { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}

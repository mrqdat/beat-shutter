using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace Img_socialmedia.Models
{
    public partial class CommentViewModel
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public int UserId { get; set; }
        public int PostId { get; set; }
        public DateTime CreateAt { get; set; }
        [NotMapped]
        public string UserImg { get; set; }
        [NotMapped]
        public string Username { get; set; }
        public virtual PostViewModel Post { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class NotificationViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TriggerUserId { get; set; }
        public int EventId { get; set; }
        public int PostId { get; set; }
        public bool Status { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual EventTypeViewModel Event { get; set; }
        public virtual PostViewModel Post { get; set; }
        public virtual UserViewModel User { get; set; }
    }
}

using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class EventTypeViewModel
    {
        public EventTypeViewModel()
        {
            Notification = new HashSet<NotificationViewModel>();
        }

        public int Id { get; set; }
        public string Description { get; set; }

        public virtual ICollection<NotificationViewModel> Notification { get; set; }
    }
}

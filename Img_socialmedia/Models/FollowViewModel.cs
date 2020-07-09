using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class FollowViewModel
    {
        public int? FollowerId { get; set; }
        public int? Followed { get; set; }

        public virtual UserViewModel Follower { get; set; }
    }
}

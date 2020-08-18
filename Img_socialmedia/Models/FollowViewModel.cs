using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class FollowViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FollowingUserId { get; set; }
    }
}

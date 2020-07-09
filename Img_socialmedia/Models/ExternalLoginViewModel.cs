using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class ExternalLoginViewModel
    {
        public string LoginProvider { get; set; }
        public string ProviderKey { get; set; }
        public int UserId { get; set; }

        public virtual UserViewModel User { get; set; }
    }
}

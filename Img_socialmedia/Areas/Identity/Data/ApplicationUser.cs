using Microsoft.AspNetCore.Identity;

namespace Img_socialmedia.Areas.Identity.Data
{
    public class ApplicationUser : IdentityUser
    {
        [PersonalData]
        public string firstname { get; set; }
        [PersonalData]
        public string lastname { get; set; }
    }
}

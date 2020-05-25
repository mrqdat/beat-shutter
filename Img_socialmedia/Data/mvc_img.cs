using Microsoft.EntityFrameworkCore;
using Img_socialmedia.Models;

namespace Img_socialmedia.Data
{
    public class mvc_img:DbContext
    {
        public mvc_img(DbContextOptions<mvc_img> options):base(options)
        {

        }

        public DbSet<UserViewModel> users { get; set; }
    }
}

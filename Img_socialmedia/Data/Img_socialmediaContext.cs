using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Img_socialmedia.Models;

namespace Img_socialmedia.Data
{
    public class Img_socialmediaContext : DbContext
    {
        public Img_socialmediaContext (DbContextOptions<Img_socialmediaContext> options)
            : base(options)
        {
        }

        public DbSet<Img_socialmedia.Models.PhotoViewModel> PhotoViewModel { get; set; }
    }
}

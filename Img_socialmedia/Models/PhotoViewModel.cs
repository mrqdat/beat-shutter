using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class PhotoViewModel
    {
        public PhotoViewModel()
        {
            CollectionDetail = new HashSet<CollectionDetailViewModel>();
            Post = new HashSet<PostViewModel>();
        }

        public int Id { get; set; }
        public string Url { get; set; }
        public string CameraModel { get; set; }
        public string Aperture { get; set; }
        public string ShutterSpeed { get; set; }
        public int? Iso { get; set; }
        public int? Width { get; set; }
        public int? Height { get; set; }
        public string Location { get; set; }
        public DateTime? CreateAt { get; set; }
        public int? FocalLength { get; set; }

        public virtual ICollection<CollectionDetailViewModel> CollectionDetail { get; set; }
        public virtual ICollection<PostViewModel> Post { get; set; }
    }
}

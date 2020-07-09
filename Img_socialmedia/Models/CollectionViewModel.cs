using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class CollectionViewModel
    {
        public CollectionViewModel()
        {
            CollectionDetail = new HashSet<CollectionDetailViewModel>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? UserId { get; set; }
        public DateTime? CreateAt { get; set; }

        public virtual UserViewModel User { get; set; }
        public virtual ICollection<CollectionDetailViewModel> CollectionDetail { get; set; }
    }
}

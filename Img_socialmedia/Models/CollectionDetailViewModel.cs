using System;
using System.Collections.Generic;

namespace Img_socialmedia.Models
{
    public partial class CollectionDetailViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CollectionId { get; set; }

        public virtual CollectionViewModel Collection { get; set; }
        public virtual PhotoViewModel Photo { get; set; }
        public virtual PostViewModel Post { get; set; }
    }
}

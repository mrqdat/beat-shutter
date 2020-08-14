using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Img_socialmedia.Models
{
    public partial class CollectionDetailViewModel
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public int CollectionId { get; set; }
        [NotMapped]
        public string PostPhoto { get; set;}
        public virtual CollectionViewModel Collection { get; set; }
        public virtual PostViewModel Post { get; set; }
    }
}

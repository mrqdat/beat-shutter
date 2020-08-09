using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Img_socialmedia.Models
{
    public class member
    {
        public CollectionDetailViewModel collectionDetailView { get; set; }
        public IEnumerable<CollectionViewModel> collectionView { get; set; }
        public IEnumerable<PostViewModel> postView { get; set; }

        public UserViewModel userView { get; set; }

    }
}

using System;
using System.Collections.Generic;

namespace ShopIt.Server.Models
{
    public partial class Advert
    {
        public Advert()
        {
            AdProducts = new HashSet<AdProduct>();
        }

        public Guid AdvertId { get; set; }
        public Guid CustomerId { get; set; }

        public virtual ICollection<AdProduct> AdProducts { get; set; }
        public virtual Advert AdvertNavigation { get; set; }
        public virtual Advert InverseAdvertNavigation { get; set; }
        public virtual Company Customer { get; set; }
    }
}

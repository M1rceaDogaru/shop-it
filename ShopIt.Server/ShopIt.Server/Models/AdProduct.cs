using System;
using System.Collections.Generic;

namespace ShopIt.Server.Models
{
    public partial class AdProduct
    {
        public AdProduct()
        {
            AdRetailers = new HashSet<AdRetailer>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid AdvertId { get; set; }
        public Guid? ProductImageId { get; set; }

        public virtual ICollection<AdRetailer> AdRetailers { get; set; }
        public virtual Advert Advert { get; set; }
        public virtual Image ProductImage { get; set; }
    }
}

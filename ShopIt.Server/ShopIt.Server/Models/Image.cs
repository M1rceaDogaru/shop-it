using System;
using System.Collections.Generic;

namespace ShopIt.Server.Models
{
    public partial class Image
    {
        public Image()
        {
            AdProduct = new HashSet<AdProduct>();
            AdRetailer = new HashSet<AdRetailer>();
        }

        public Guid ImageId { get; set; }
        public byte[] ImageData { get; set; }

        public virtual ICollection<AdProduct> AdProduct { get; set; }
        public virtual ICollection<AdRetailer> AdRetailer { get; set; }
    }
}

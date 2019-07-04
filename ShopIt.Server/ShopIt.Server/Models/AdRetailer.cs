using System;
using System.Collections.Generic;

namespace ShopIt.Server.Models
{
    public partial class AdRetailer
    {
        public Guid Id { get; set; }
        public Guid AdProductId { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string CountryCode { get; set; }
        public Guid? RetailerLogoId { get; set; }

        public virtual AdProduct AdProduct { get; set; }
        public virtual Image RetailerLogo { get; set; }
    }
}

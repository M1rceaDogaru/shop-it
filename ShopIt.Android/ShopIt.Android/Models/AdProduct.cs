using System.Collections.Generic;

namespace ShopIt.Models
{
    public class AdProduct
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AdRetailer> Retailers { get; set; }
    }
}
using System.Collections.Generic;

namespace ShopIt.Server.ViewModels
{
    public class AdProductViewModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<AdRetailerViewModel> Retailers { get; set; }
    }
}

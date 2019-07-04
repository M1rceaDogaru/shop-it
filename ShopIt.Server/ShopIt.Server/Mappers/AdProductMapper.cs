using ShopIt.Server.Models;
using ShopIt.Server.ViewModels;
using System.Linq;

namespace ShopIt.Server.Mappers
{
    public static class AdProductMapper
    {
        public static AdvertViewModel ToViewModel(this Advert advert)
        {
            return new AdvertViewModel()
            {
                Products = advert.AdProducts.Select(product => product.ToViewModel()).ToList()
            };
        }

        public static AdProductViewModel ToViewModel(this AdProduct product)
        {
            return new AdProductViewModel()
            {
                Name = product.Name,
                Description = product.Description,
                Retailers = product.AdRetailers.Select(retailer => retailer.ToViewModel()).ToList()
            };
        }

        public static AdRetailerViewModel ToViewModel(this AdRetailer retailer)
        {
            return new AdRetailerViewModel()
            {
                Name = retailer.Name,
                Url = retailer.Url
            };
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ShopIt.Server.Mappers;
using ShopIt.Server.Models;
using ShopIt.Server.ViewModels;
using System;
using System.Linq;

namespace ShopIt.Server.Controllers
{
    [Route("api/[controller]")]
    public class AdController : Controller
    {
        private readonly ShopItContext _context;
        public AdController(ShopItContext context)
        {
            _context = context;
        }

        // GET api/ad/5
        [HttpGet("{id}")]
        public JsonResult Get(Guid id)
        {
            var advert = _context.Advert.Include(ad => ad.AdProducts).ThenInclude(product => product.AdRetailers).FirstOrDefault(product => product.AdvertId == id);
            if (advert != null)
            {
                return new JsonResult(advert.ToViewModel());
            }

            return new JsonResult(new ErrorViewModel() { Message = "No advert data found for given id" });
        }        
    }
}

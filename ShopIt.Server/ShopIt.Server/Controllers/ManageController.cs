using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopIt.Server.Controllers
{
    [Route("[controller]")]
    public class ManageController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}

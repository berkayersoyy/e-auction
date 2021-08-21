using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace EAuction.UI.Controllers
{
    [Authorize]
    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

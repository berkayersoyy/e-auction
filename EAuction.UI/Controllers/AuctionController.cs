using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using EAuction.UI.ViewModel;

namespace EAuction.UI.Controllers
{

    public class AuctionController : Controller
    {
        public IActionResult Index()
        {
            List<AuctionViewModel> model = new List<AuctionViewModel>();
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(AuctionViewModel model)
        {
            return View(model);
        }

        public IActionResult Detail()
        {
            return View();
        }
    }
}

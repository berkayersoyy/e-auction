using Microsoft.AspNetCore.Mvc;

namespace EAuction.UI.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

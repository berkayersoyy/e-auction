using System.Threading.Tasks;
using EAuction.Core.Entities;
using EAuction.UI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EAuction.UI.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        private UserManager<AppUser> _userManager { get; }
        private SignInManager<AppUser> _signInManager { get; }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginModel)
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Signup(AppUserViewModel signupModel)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.FirstName = signupModel.FirstName;
                user.LastName = signupModel.LastName;
                user.Email = signupModel.Email;
                user.PhoneNumber = signupModel.PhoneNumber;
                user.UserName = signupModel.UserName;
                if (signupModel.UserSelectTypeId == 1)
                {
                    user.IsBuyer = true;
                    user.IsSeller = false;
                }
                else
                {
                    user.IsSeller = true;
                    user.IsBuyer = false;
                }

                var result = await _userManager.CreateAsync(user, signupModel.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError identityError in result.Errors)
                    {
                        ModelState.AddModelError("",identityError.Description);
                    }
                }
            }
            return View(signupModel);
        }
    }
}

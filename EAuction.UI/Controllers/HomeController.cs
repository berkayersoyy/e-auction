using System.Threading.Tasks;
using EAuction.Core.Entities;
using EAuction.UI.ViewModel;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<IActionResult> Login(LoginViewModel loginModel,string returnUrl)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(loginModel.Email);
                if (user!=null)
                {
                    await _signInManager.SignOutAsync();
                    var result = await _signInManager.PasswordSignInAsync(user, loginModel.Password,false,false);
                    if (result.Succeeded)
                    {
                        //return RedirectToAction("Index");
                        return LocalRedirect(returnUrl);
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email address is not valid or password");
                    }
                }
                else
                {
                    ModelState.AddModelError("","Email address is not valid or password");
                }
            }
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

        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }
    }
}

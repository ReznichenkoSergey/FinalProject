using FinalProject.Infrastructure.Services.Interfaces;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        public SignInManager<IdentityUser> _signInManager;
        public UserManager<IdentityUser> _userManager;
        IMessageService _messageService;


        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager, IMessageService messageService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _messageService = messageService;
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register([FromForm] AccountRegisterViewModel account)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    UserName = account.EMail,
                    Email = account.EMail
                };
                var result = _userManager.CreateAsync(user, account.Password).Result;
                if (result.Succeeded)
                {
                    _signInManager.SignInAsync(user, false).GetAwaiter().GetResult();
                    _messageService.SendMessage(account.EMail);
                    return RedirectToAction("GetAllCars", "Car");
                }
                else
                    result.Errors.ToList().ForEach(x => ModelState.AddModelError(x.Code, x.Description));
            }
            return View();
        }

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(AccountLoginViewModel login, [FromQuery] string returnUri)
        {
            if (ModelState.IsValid)
            {
                var sighInTask = _signInManager.PasswordSignInAsync(login.EMail, login.Password, false, false).GetAwaiter().GetResult();
                if (sighInTask.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUri))
                    {
                        return Redirect(returnUri);
                    }
                    return RedirectToAction("GetAllDealers", "CarDealer");
                }
                ModelState.AddModelError("", "Incorrect security pair!");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync().GetAwaiter().GetResult();
            return RedirectToAction("Index", "Home");
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FinalProject.Models.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace FinalProject.Controllers
{
    public class AccountController : Controller
    {
        public SignInManager<IdentityUser> _signInManager;
        public UserManager<IdentityUser> _userManager;


        public AccountController(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IActionResult> RegisterAsync([FromForm] AccountLoginViewModel account)
        {
            if(ModelState.IsValid)
            {
                var user = new IdentityUser()
                {
                    Email = account.EMail,
                    UserName = account.UserName
                };
                var result = await _userManager.CreateAsync(user, account.Password);
                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("GetAllCarsAsync", "Car");
                }
            }
            return View();
        }
    }
}

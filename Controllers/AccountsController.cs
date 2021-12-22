using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using webapp.Models;


namespace webapp.Controllers
{
    public class AccountsController : Controller
    {
        private readonly ILogger<AccountsController> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountsController(
            ILogger<AccountsController> logger, 
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
            public IActionResult Login()
            {
                return View();
            }

            [HttpPost]
            
            public async Task<IActionResult> Login(Login model)
            {
                try
                {
                     var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false,
                     lockoutOnFailure: false);

                     if (result.Succeeded)
                     {
                         return RedirectToAction("Index", "Home");
                     }
                     else
                     {
                         ModelState.AddModelError(string.Empty, "Invalid login attemp. Email or Password is wrong");
                         return View();
                     }
                }
                catch (System.Exception)
                {
                    
                    throw;
                }
            }

            public IActionResult Register()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]

            public async Task<IActionResult> Register(Register model)
            {
                if (!ModelState.IsValid) return View();

                try
                {
                    ApplicationUser user = await _userManager.FindByEmailAsync(model.Email);
                         if (user == null)
                         {
                             user = new ApplicationUser();
                             user.UserName = model.Email;
                             user.Email = model.Email;

                             IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                             ViewBag.Message = "User was Created";

                             if (result.Succeeded)
                             {
                                 await _userManager.AddToRoleAsync(user, "user");
                                 return RedirectToAction("Login");
                             }
                             else
                             {
                                 ModelState.AddModelError("", "Invalid user details. Add at least One Uppercase, Lowercase, Special Character and Number!");
                                 return View();
                             }

                         }
                         else
                         {
                             ViewBag.Message = "User already Registered";
                             return View();
                         }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(ex.Message,null);
                    return View();
                }
             }

             public async Task<IActionResult> Logout()
             {
                 await _signInManager.SignOutAsync();
                 return RedirectToAction("Index", "Home");
             }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.
            TraceIdentifier });
        }
        
    }
}



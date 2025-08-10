using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZayShop.Models;
using ZayShop.Models.Data;

namespace ZayShop.Controllers
{
    public class AccountController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        public AccountController(AppDbContext context, SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _signInManager = signInManager;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login(string returnUrl=null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel, string returnUrl)
        {
            if (loginModel == null)
            {
                ModelState.AddModelError("", "Invalid login attempt.");
                return View(loginModel);
            }
           
                var user = _userManager.FindByEmailAsync(loginModel.Email).Result;
                if (user != null)
                {
                    var result = _signInManager.PasswordSignInAsync(user, loginModel.Password, false, false).Result;
                    if (result.Succeeded)
                    {
                        if (!string.IsNullOrEmpty(returnUrl))
                            return Redirect(returnUrl);
                        return RedirectToAction("Index", "Home");
                    }
                    ModelState.AddModelError("", "Invalid login attempt.");
                }
                else
                {
                    ModelState.AddModelError("", "User not found.");
                }
            
            return View(loginModel);
        }
    }
}

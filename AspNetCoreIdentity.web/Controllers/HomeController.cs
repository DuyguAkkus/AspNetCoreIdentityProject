using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentitiy.web.Models;
using AspNetCoreIdentitiy.web.ViewModels;
using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentitiy.web.Extensions;

namespace AspNetCoreIdentitiy.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _logger = logger;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            if (User.Identity?.IsAuthenticated ?? false) // Kullanıcı giriş yapmış mı?
            {
                var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
                if (currentUser != null)
                {
                    var roles = await _userManager.GetRolesAsync(currentUser);
                    ViewBag.UserName = currentUser.UserName;
                    ViewBag.UserRole = roles.Count > 0 ? roles[0] : "Üye"; // İlk rolü al, yoksa "Üye" göster
                }
            }
            return View();
        }

        public IActionResult About() => View();
        public IActionResult Privacy() => View();
        public IActionResult SignUp() => View();
        public IActionResult SignIn() => View();

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var identityResult = await _userManager.CreateAsync(
                new() { UserName = request.UserName, PhoneNumber = request.Phone, Email = request.Email },
                request.ConfirmPassword
            );

            if (!identityResult.Succeeded)
            {
                ModelState.AddModelErrorList(identityResult.Errors.Select(x => x.Description).ToList());
                return View();
            }

            TempData["SuccessMessage"] = "Üyelik kayıt işlemi başarıyla gerçekleşmiştir.";
            return RedirectToAction(nameof(HomeController.SignIn));
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(SignInViewModel model, string? returnUrl = null)
        {
            returnUrl ??= Url.Action("Index", "Home"); // Eğer `returnUrl` boşsa `/Member/Index`'e yönlendir

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var hasUser = await _userManager.FindByEmailAsync(model.Email);

            if (hasUser == null)
            {
                ModelState.AddModelError(string.Empty, "Email veya şifre yanlış.");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(hasUser, model.Password, model.RememberMe, true);

            if (result.Succeeded)
            {
                return Redirect(returnUrl); // ✅ Giriş başarılıysa `/Member/Index`'e yönlendirilir
            }

            if (result.IsLockedOut)
            {
                ModelState.AddModelError(string.Empty, "3 dakika sonra tekrar deneyiniz.");
                return View(model);
            }

            ModelState.AddModelError(string.Empty, "Email veya şifre yanlış.");
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

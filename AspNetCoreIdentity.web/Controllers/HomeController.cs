using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentitiy.web.Models;
using AspNetCoreIdentitiy.web.ViewModels;
using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentitiy.web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager; // ✅ UserManager eklendi

        // ✅ UserManager parametre olarak alınıp constructor içinde atandı
        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager)); // 🔥 Null kontrolü eklendi
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult SignIn()
        {
            return View();
        }
        
        

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpViewModel request)
        {
            if (!ModelState.IsValid) // Form alanlarını doğrula
            {
                return View(request); // Hataları tekrar göster
            }

            var newUser = new AppUser
            {
                UserName = request.UserName,
                PhoneNumber = request.Phone,
                Email = request.Email
            };

            var identityResult = await _userManager.CreateAsync(newUser, request.Password);

            if (identityResult.Succeeded)
            {
                TempData["SuccessMessage"] = "Kayıt başarıyla gerçekleşti!";
                return RedirectToAction("SignUp");
            }

            foreach (var error in identityResult.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(request);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

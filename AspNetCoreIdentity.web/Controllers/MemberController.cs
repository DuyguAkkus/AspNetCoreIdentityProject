using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentitiy.web.Models;
using AspNetCoreIdentitiy.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;

namespace AspNetCoreIdentitiy.Web.Controllers
{
    [Authorize] // Sadece giriş yapan üyelerin erişebilmesi için
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager) 
        {
            _signInManager = signInManager;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }

        public async Task<IActionResult> Index()
        {
            if (!User.Identity?.IsAuthenticated ?? true)
            {
                return RedirectToAction("SignIn", "Home");
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var userViewModel = new UserViewModels
            {
                Email = currentUser.Email ?? "Email bulunamadı",
                PhoneNumber = currentUser.PhoneNumber ?? "Telefon bilgisi yok",
                UserName = currentUser.UserName ?? "Kullanıcı adı bulunamadı"
            };

            return View(userViewModel); // ✅ Modeli View'e gönder
        }
        
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); // ✅ Çıkış sonrası anasayfaya yönlendir
        }
    }
}
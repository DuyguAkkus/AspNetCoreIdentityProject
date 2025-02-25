
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentitiy.web.Models;

namespace AspNetCoreIdentitiy.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        public MemberController(SignInManager<AppUser> signInManager) // ✅ `IdentityUser` yerine `AppUser` kullanıldı.
        {
            _signInManager = signInManager;
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync(); // ✅ Çıkış işlemi başarılı bir şekilde çalışacak.

        }
    }
}
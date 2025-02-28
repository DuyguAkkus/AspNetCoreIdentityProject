using AspNetCoreIdentitiy.web.Areas.Admin.Models;
using AspNetCoreIdentitiy.web.Extensions;
using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Linq;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    // [Authorize(Roles = "Admin")] // Yalnızca Admin rolüne sahip kullanıcılar erişebilir
    public class RolesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Index()
        {
            var roles = await _roleManager.Roles
                .Select(x => new RoleViewModel
                {
                    Id = x.Id,
                    Name = x.Name!
                })
                .ToListAsync();

            return View(roles);
        }

        public IActionResult RoleCreate() // GET metodu
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RoleCreate(RoleCreateViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request); // Form verilerini kaybetmemek için
            }

            var result = await _roleManager.CreateAsync(new AppRole { Name = request.RoleName });

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View(request); // Hata mesajları ile birlikte sayfa tekrar yüklenir
            }

            return RedirectToAction(nameof(Index)); // Başarılı olursa Index sayfasına yönlendir
        }
    }
}
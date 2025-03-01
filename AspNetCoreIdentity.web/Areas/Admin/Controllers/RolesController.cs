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
    //[Authorize(Roles = "Admin")] // Yalnızca Admin rolüne sahip kullanıcılar erişebilir
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

            //  Eğer girilen rol adı zaten varsa hata mesajı göster
            var roleExists = await _roleManager.RoleExistsAsync(request.RoleName);
            if (roleExists)
            {
                ModelState.AddModelError("", "Bu rol adı zaten kullanımda, lütfen başka bir isim girin.");
                return View(request);
            }

            var result = await _roleManager.CreateAsync(new AppRole { Name = request.RoleName });

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    if (error.Code == "DuplicateRoleName")
                    {
                        ModelState.AddModelError("", "Bu rol adı zaten kullanımda, farklı bir ad girin.");
                    }
                    else
                    {
                        ModelState.AddModelError("", error.Description); // Varsayılan hata mesajını Türkçeleştir
                    }
                }
                return View(request); // Hata mesajları ile birlikte sayfa tekrar yüklenir
            }

            TempData["SuccessMessage"] = "Rol başarıyla eklendi.";
            return RedirectToAction(nameof(Index)); // Başarılı olursa Index sayfasına yönlendir
        }


        public async Task<IActionResult> RoleUpdate(string id) // GET metodu
        {
            if (string.IsNullOrEmpty(id))
            {
                TempData["ErrorMessage"] = "Geçersiz rol ID.";
                return RedirectToAction("Index");
            }

            var roleToUpdate = await _roleManager.FindByIdAsync(id);
            if (roleToUpdate == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek rol bulunamadı.";
                return RedirectToAction("Index");
            }

            return View(new RoleUpdateViewModel { Id = roleToUpdate.Id, Name = roleToUpdate.Name!});
        }

        [HttpPost]
        public async Task<IActionResult> RoleUpdate(RoleUpdateViewModel request) // POST metodu
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var roleToUpdate = await _roleManager.FindByIdAsync(request.Id);
            if (roleToUpdate == null)
            {
                TempData["ErrorMessage"] = "Güncellenecek rol bulunamadı.";
                return RedirectToAction("Index");
            }

            roleToUpdate.Name = request.Name;
            var result = await _roleManager.UpdateAsync(roleToUpdate);

            if (!result.Succeeded)
            {
                ModelState.AddModelErrorList(result.Errors);
                return View(request); // Hata varsa tekrar sayfayı göster
            }

            TempData["SuccessMessage"] = "Rol başarıyla güncellendi.";
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> RoleDelete(string id)
        {
            var roleToDelete = await _roleManager.FindByIdAsync(id);

            if (roleToDelete == null)
            {
                throw new Exception("Silinecek rol bulunamamıştır.");
            }

            var result = await _roleManager.DeleteAsync(roleToDelete);

            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.Select(x => x.Description).First());
            }

            TempData["SuccessMessage"] = "Rol silinmiştir";
            return RedirectToAction(nameof(RolesController.Index));
            
        }
        
        
        
        
        
        
        
        
    }
    
    
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentitiy.web.Models;
using AspNetCoreIdentitiy.web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using AspNetCoreIdentitiy.web.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.FileProviders;

namespace AspNetCoreIdentitiy.Web.Controllers
{
    [Authorize] // Sadece giriş yapan üyelerin erişebilmesi için
    public class MemberController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly IFileProvider _fileProvider;

        public MemberController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IFileProvider fileProvider) 
        {
            _signInManager = signInManager;
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _fileProvider = fileProvider;
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
                UserName = currentUser.UserName ?? "Kullanıcı adı bulunamadı",
                PictureUrl = string.IsNullOrEmpty(currentUser.Picture) ? "/userPictures/default_user.jpg" : currentUser.Picture
            };

            return View(userViewModel);
        }

        public async Task<IActionResult> UserEdit()
        {
            ViewBag.genderList = new SelectList(Enum.GetValues(typeof(Gender)));
            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            var userEditViewModel = new UserEditViewModel()
            {
                Email = currentUser.Email!,
                PhoneNumber = currentUser.PhoneNumber!,
                UserName = currentUser.UserName!,
                BirthDate = currentUser.Birthdate,
                Gender = currentUser.Gender,
                City = currentUser.City,
            };

            return View(userEditViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UserEdit(UserEditViewModel request)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var currentUser = await _userManager.FindByNameAsync(User.Identity.Name);
            if (currentUser == null)
            {
                return NotFound("Kullanıcı bulunamadı.");
            }

            currentUser.UserName = request.UserName;
            currentUser.Email = request.Email;
            currentUser.PhoneNumber = request.PhoneNumber;
            currentUser.Gender = request.Gender;
            currentUser.City = request.City;
            currentUser.Birthdate = request.BirthDate;

            if (request.ProfilePicture != null && request.ProfilePicture.Length > 0)
            {
                var wwwRootPath = _fileProvider.GetDirectoryContents("wwwroot")
                    .FirstOrDefault(x => x.Name == "userPictures")?.PhysicalPath;

                if (string.IsNullOrEmpty(wwwRootPath))
                {
                    return BadRequest("Profil resmi kaydedilecek dizin bulunamadı.");
                }

                // Önceki resmi silme
                if (!string.IsNullOrEmpty(currentUser.Picture))
                {
                    var oldPicturePath = Path.Combine(wwwRootPath, Path.GetFileName(currentUser.Picture));
                    if (System.IO.File.Exists(oldPicturePath))
                    {
                        System.IO.File.Delete(oldPicturePath);
                    }
                }

                var randomFileName = $"{Guid.NewGuid()}{Path.GetExtension(request.ProfilePicture.FileName)}";
                var newPicturePath = Path.Combine(wwwRootPath, randomFileName);

                using (var stream = new FileStream(newPicturePath, FileMode.Create))
                {
                    await request.ProfilePicture.CopyToAsync(stream);
                }

                currentUser.Picture = $"/userPictures/{randomFileName}";
            }

            var updateToUserResult = await _userManager.UpdateAsync(currentUser);
            if (!updateToUserResult.Succeeded)
            {
                ModelState.AddModelErrorList(updateToUserResult.Errors);
                return View(request);
            }
            
            await _userManager.UpdateSecurityStampAsync(currentUser);
            await _signInManager.SignOutAsync();
            await _signInManager.SignInAsync(currentUser, true);

            TempData["SuccessMessage"] = "Üye bilgileri başarıyla değiştirildi.";
            return RedirectToAction("Index", "Member");
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home"); 
        }
    }
}

using AspNetCoreIdentitiy.web.Areas.Admin.Models;
using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Controllers;
[Area("Admin")]
[Authorize(Roles = "Admin")] 
public class HomeController : Controller
{
    private readonly UserManager<AppUser>_UserManager;

    public HomeController( UserManager<AppUser> userManager )
    {
        _UserManager = userManager;
    }
    public IActionResult Index() 
    {
        return View();
    }
    
    public async Task<IActionResult> UserList()
    {
        var userList = await _UserManager.Users.ToListAsync(); // Kullanıcıları getiriyoruz.

        var userViewModelList = new List<UserViewModel>();

        foreach (var user in userList)
        {
            var roles = await _UserManager.GetRolesAsync(user); // Kullanıcının rollerini çekiyoruz.

            userViewModelList.Add(new UserViewModel
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.UserName,
                Roles = roles.ToList() // Kullanıcının rollerini ekledik.
            });
        }

        return View(userViewModelList); // Modeli View'e gönderiyoruz.
    }

}
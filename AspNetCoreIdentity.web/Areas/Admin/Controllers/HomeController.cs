using AspNetCoreIdentitiy.web.Areas.Admin.Models;
using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Controllers;
[Area("Admin")]
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
        var userList = await _UserManager.Users.ToListAsync();
    
        var userViewModelList = userList.Select(x => new UserViewModel
        {
            Id = x.Id, 
            Email = x.Email,
            Name = x.UserName 
        }).ToList(); 

        return View(userViewModelList); // Modeli View'e gönder
    }

}
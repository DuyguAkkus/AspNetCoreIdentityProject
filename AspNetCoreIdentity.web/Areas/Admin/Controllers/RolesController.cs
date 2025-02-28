using AspNetCoreIdentitiy.web.Areas.Admin.Models;
using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class RolesController : Controller  // Controller sınıfından miras almalı!
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public RolesController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult RoleCreate(RoleCreateViewModel request)
        {
            return View();
        }
        
        
    }
}
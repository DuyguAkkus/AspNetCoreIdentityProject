using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Controllers;

public class FormController : Controller
{
    public IActionResult Index() 
    {
        return View(); 
    }
}

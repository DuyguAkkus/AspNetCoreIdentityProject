using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Student.Controllers;

public class HomeController :Controller
{
    public IActionResult StudentIndex() 
    {
        return View(); 
    }
}
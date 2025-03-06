using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Teacher.Controllers;

public class HomeController : Controller
{
    public IActionResult TeacherIndex() 
    {
        return View(); // ✅ Views/Home/Index.cshtml'yi çağırır
    }
}
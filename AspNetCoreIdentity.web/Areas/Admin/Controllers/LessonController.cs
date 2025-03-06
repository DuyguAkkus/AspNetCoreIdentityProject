using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Controllers;

public class LessonController :Controller
{
    public IActionResult Index() 
    {
        return View(); // ✅ Views/Home/Index.cshtml'yi çağırır
    }
}
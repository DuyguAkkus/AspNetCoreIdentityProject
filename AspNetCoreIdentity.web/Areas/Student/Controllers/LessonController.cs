using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Student.Controllers;

public class LessonController : Controller
{
    public IActionResult StudentLessons()
    {
        return View();
    }
}
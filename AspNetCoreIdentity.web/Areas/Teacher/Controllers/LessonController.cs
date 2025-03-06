using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Teacher.Controllers;

public class LessonController : Controller
{
    public IActionResult MyLeassons() 
    {
        return View(); 
    }
    
    public IActionResult Create() 
    {
        return View(); 
    }
}
using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Student.Controllers;

public class ExamController : Controller
{
    public IActionResult MyExams() 
    {
        return View(); 
    }
}
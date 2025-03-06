using Microsoft.AspNetCore.Mvc;

namespace AspNetCoreIdentitiy.web.Student.Controllers;


public class CertificateController :Controller
{
    public IActionResult MyCertificates() 
    {
        return View(); 
    }
}
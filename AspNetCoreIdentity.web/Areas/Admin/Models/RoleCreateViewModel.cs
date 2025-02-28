using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Models;

public class RoleCreateViewModel
{
    [Required(ErrorMessage = "Rol adı gereklidir!")]
    public string RoleName { get; set; }
}
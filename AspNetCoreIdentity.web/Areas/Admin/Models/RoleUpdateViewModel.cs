using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentitiy.web.Areas.Admin.Models;

public class RoleUpdateViewModel
{
    
    [Required(ErrorMessage = "Role isim alanı boş bırakılamaz.")]
    [Display(Name = "Role ismi :")]
    public string Name { get; set; } = null!;
    public string Id { get; set; } = null!;
}
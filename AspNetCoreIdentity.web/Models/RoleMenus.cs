namespace AspNetCoreIdentitiy.web.Models;

public class RoleMenus
{
    public int Id { get; set; } // Benzersiz kimlik
    public string RoleId { get; set; } = null!; // Rol kimliği (FK)
    public AppRole Role { get; set; } = null!; // Rol referansı
    public int MenuId { get; set; } // Menü kimliği (FK)
    public Menu Menu { get; set; } = null!; // Menü referansı
}
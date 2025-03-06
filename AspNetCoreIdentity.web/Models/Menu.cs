namespace AspNetCoreIdentitiy.web.Models;

public class Menu
{
    public int Id { get; set; } // Menü benzersiz kimliği
    public string Name { get; set; } = null!; // Menü adı
    public string ControllerName { get; set; } = null!; // Controller adı
    public string ActionName { get; set; } = null!; // Action adı
    public string Icon { get; set; } = "fa-home"; // Menü ikonu
    public int SortOrder { get; set; } = 1; // Menü sırası
    public int AuthorityLevel { get; set; } // Yetki seviyesi (0-4)
}

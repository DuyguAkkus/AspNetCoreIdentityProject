namespace AspNetCoreIdentitiy.web.Areas.Admin.Models
{
    public class UserViewModel
    {
        public string Id { get; set; } = string.Empty; // Nullable yerine boş string atandı
        
        public string? Name { get; set; } = string.Empty; // Name yerine UserName olarak değiştirildi
        
        public string? Email { get; set; } = string.Empty; // Nullable yerine boş string atandı
    }
}
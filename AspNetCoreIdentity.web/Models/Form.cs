namespace AspNetCoreIdentitiy.web.Models;

public class Form
{
    public int Id { get; set; }  // Form benzersiz kimliği
    public string Name { get; set; } = null!; // Formun adı
    public string Description { get; set; } = null!; // Açıklama
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Oluşturulma tarihi
    public string CreatedById { get; set; } = null!; // Admin kullanıcı kimliği (FK)
    public AppUser CreatedBy { get; set; } = null!; // Admin kullanıcı referansı
}
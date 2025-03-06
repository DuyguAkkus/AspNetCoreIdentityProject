namespace AspNetCoreIdentitiy.web.Models;

public class Certificate
{
    public int Id { get; set; }  // Sertifika benzersiz kimliği
    public string CertificateName { get; set; } = null!; // Sertifika adı
    public string UserId { get; set; } = null!; // Sertifikayı alan öğrenci kimliği (FK)
    public AppUser User { get; set; } = null!; // Öğrenci referansı
    public DateTime IssueDate { get; set; } // Sertifikanın verildiği tarih
}
namespace AspNetCoreIdentitiy.web.Models;

public class Lesson
{
    public int Id { get; set; }  // Ders benzersiz kimliği
    public string Name { get; set; } = null!; // Ders adı
    public string Description { get; set; } = null!; // Açıklama
    public string InstructorId { get; set; } = null!; // Eğitmen kimliği (FK)
    public AppUser Instructor { get; set; } = null!; // Eğitmen referansı
    public DateTime StartDate { get; set; } // Ders başlangıç tarihi
    public DateTime EndDate { get; set; } // Ders bitiş tarihi
}
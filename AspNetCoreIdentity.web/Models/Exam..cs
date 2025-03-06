namespace AspNetCoreIdentitiy.web.Models;

public class Exam
{
    public int Id { get; set; }  // Sınav benzersiz kimliği
    public string ExamName { get; set; } = null!; // Sınav adı
    public int LessonId { get; set; }  // Hangi derse ait sınav (FK)
    public Lesson Lesson { get; set; } = null!; // Ders referansı
    public DateTime ExamDate { get; set; } // Sınav tarihi
}
using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Menu> Menus { get; set; }
    public DbSet<RoleMenus> RoleMenus { get; set; }
    public DbSet<Form> Forms { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Exam> Exams { get; set; }
    public DbSet<Certificate> Certificates { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // 🔹 RoleMenus ilişkileri
        builder.Entity<RoleMenus>()
            .HasOne(rm => rm.Role)
            .WithMany()
            .HasForeignKey(rm => rm.RoleId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<RoleMenus>()
            .HasOne(rm => rm.Menu)
            .WithMany()
            .HasForeignKey(rm => rm.MenuId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔹 Form ilişkileri
        builder.Entity<Form>()
            .HasOne(f => f.CreatedBy)
            .WithMany()
            .HasForeignKey(f => f.CreatedById)
            .OnDelete(DeleteBehavior.Restrict); // Admin silinirse formlar kalmaya devam eder

        // 🔹 Lesson ilişkileri
        builder.Entity<Lesson>()
            .HasOne(l => l.Instructor)
            .WithMany()
            .HasForeignKey(l => l.InstructorId)
            .OnDelete(DeleteBehavior.Restrict); // Eğitmen silinirse dersler kalmaya devam eder

        // 🔹 Exam ilişkileri
        builder.Entity<Exam>()
            .HasOne(e => e.Lesson)
            .WithMany()
            .HasForeignKey(e => e.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        // 🔹 Certificate ilişkileri
        builder.Entity<Certificate>()
            .HasOne(c => c.User)
            .WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}

using AspNetCoreIdentitiy.web.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreIdentitiy.web.Data;

public static class MenuSeeder
{
    public static async Task SeedMenus(AppDbContext context)
    {
        if (!await context.Menus.AnyAsync()) // Eğer tablo boşsa verileri ekle
        {
            var menus = new List<Menu>
            {
                new Menu { Name = "Anasayfa", ControllerName = "Home", ActionName = "Index", Icon = "fa-home", SortOrder = 1, AuthorityLevel = 0 },

                // 📌 Admin Menüsü
                new Menu { Name = "Form İşlemleri", ControllerName = "Form", ActionName = "Index", Icon = "fa-file", SortOrder = 2, AuthorityLevel = 2 },
                new Menu { Name = "Rol İşlemleri", ControllerName = "Roles", ActionName = "Index", Icon = "fa-user-shield", SortOrder = 3, AuthorityLevel = 2 },
                new Menu { Name = "Ders Ekle", ControllerName = "Lesson", ActionName = "Create", Icon = "fa-plus", SortOrder = 4, AuthorityLevel = 2 }, // ✅ Admin'e ders ekleme yetkisi

                // 📌 Eğitmen Menüsü
                new Menu { Name = "Derslerim", ControllerName = "Lesson", ActionName = "MyLessons", Icon = "fa-book", SortOrder = 5, AuthorityLevel = 3 },
                new Menu { Name = "Ders Ekle", ControllerName = "Lesson", ActionName = "Create", Icon = "fa-plus", SortOrder = 6, AuthorityLevel = 3 },

                // 📌 Öğrenci Menüsü
                new Menu { Name = "Sertifikalarım", ControllerName = "Certificate", ActionName = "MyCertificates", Icon = "fa-certificate", SortOrder = 7, AuthorityLevel = 4 },
                new Menu { Name = "Sınavlarım", ControllerName = "Exam", ActionName = "MyExams", Icon = "fa-file-alt", SortOrder = 8, AuthorityLevel = 4 },
                new Menu { Name = "Derslerim", ControllerName = "Lesson", ActionName = "StudentLessons", Icon = "fa-book", SortOrder = 9, AuthorityLevel = 4 }
            };

            await context.Menus.AddRangeAsync(menus);
            await context.SaveChangesAsync();
        }
    }
}

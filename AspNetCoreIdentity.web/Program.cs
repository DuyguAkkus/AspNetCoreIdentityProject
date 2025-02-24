using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentitiy.web.Models; // AppDbContext için gerekli namespace
using AspNetCoreIdentitiy.web.Extenisons; // **Identity uzantılarını ekleme**
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// **📌 SQL Server Bağlantısını Ekle**
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// **📌 Identity Servislerini Konfigüre Et**
builder.Services.AddIdentityWithExt(); // **Özel Identity yapılandırmasını ekledik**

// **📌 MVC İçin Servisleri Ekleyelim**
builder.Services.AddControllersWithViews(); // MVC kullanımı için gerekli servisler
var app = builder.Build();

// **📌 Veritabanı Bağlantısını Test Et**
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        Console.WriteLine("🔄 Veritabanına bağlanmaya çalışılıyor...");

        if (dbContext.Database.CanConnect()) // Veritabanına bağlanabiliyor mu kontrol et
        {
            Console.WriteLine("✅ Veritabanı bağlantısı başarılı!");
        }
        else
        {
            Console.WriteLine("❌ Veritabanına bağlanılamadı!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"❌ Veritabanı bağlantı hatası: {ex.Message}");
    }
}

// **📌 Hata Yönetimi ve Güvenlik Ayarları**
if (!app.Environment.IsDevelopment()) // Eğer uygulama production ortamındaysa hata yönetimini ayarla
{
    app.UseExceptionHandler("/Home/Error"); // Hata sayfasına yönlendirme
    app.UseHsts(); // HSTS güvenlik önlemini uygula
}

// **📌 Middleware Konfigürasyonu**
app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e yönlendir
app.UseStaticFiles(); // **Statik dosyaları (CSS, JS, resimler) etkinleştir**
app.UseRouting(); // MVC yönlendirme mekanizmasını etkinleştir
app.UseAuthentication(); // **Kimlik doğrulamayı etkinleştir**
app.UseAuthorization(); // Yetkilendirme mekanizmasını etkinleştir

// **📌 Alan Destekli Rota Tanımlama**
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
).WithMetadata(new { Description = "Alan bazlı yönlendirme aktif!" });

// **📌 Varsayılan Rota Tanımı**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

// **📌 Uygulamayı Çalıştır**
app.Run();

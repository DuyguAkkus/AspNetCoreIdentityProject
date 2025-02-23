using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentitiy.web.Models; // AppDbContext için gerekli namespace

var builder = WebApplication.CreateBuilder(args);

// **SQL Server Bağlantısını Ekle**
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


// **MVC İçin Servisleri Ekleyelim**
builder.Services.AddControllersWithViews(); // MVC kullanımı için gerekli servis
builder.Services.AddIdentity<AppUser,AppRole>().AddEntityFrameworkStores<AppDbContext>();
var app = builder.Build();

// **Veritabanı Bağlantısını Test Et**
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

// **Hata Yönetimi ve Güvenlik Ayarları**
if (!app.Environment.IsDevelopment()) // Eğer uygulama production ortamındaysa hata yönetimini ayarla
{
    app.UseExceptionHandler("/Home/Error"); // Hata sayfasına yönlendirme
    app.UseHsts(); // HSTS güvenlik önlemini uygula
}

// **Middleware Konfigürasyonu**
app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e yönlendir
app.UseRouting(); // MVC yönlendirme mekanizmasını etkinleştir
app.UseAuthorization(); // Yetkilendirme mekanizmasını etkinleştir


app.MapControllerRoute(
        name: "areas",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}") 
    .WithStaticAssets(); 


app.MapStaticAssets(); // Statik dosyaları (CSS, JS, resimler) yönet

// **Varsayılan Rota Tanımı**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}") // Varsayılan olarak HomeController ve Index metodu çalışacak
    .WithStaticAssets(); // Statik dosyalarla uyumlu hale getir

app.Run(); // Uygulamayı çalıştır

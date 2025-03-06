using AspNetCoreIdentitiy.web.ClaimsProvider;
using AspNetCoreIdentitiy.web.Data;
using Microsoft.EntityFrameworkCore;
using AspNetCoreIdentitiy.web.Models; // AppDbContext için gerekli namespace
using AspNetCoreIdentitiy.web.Extenisons;
using Microsoft.AspNetCore.Authentication; // **Identity uzantılarını ekleme**
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;

var builder = WebApplication.CreateBuilder(args);

// SQL Server Bağlantısını Ekle
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityWithExt(); // **Özel Identity yapılandırmasını ekledik**

builder.Services.AddScoped<IClaimsTransformation, UserClaimProvider>();

builder.Services.AddControllersWithViews(); // MVC kullanımı için gerekli servisler

builder.Services.AddAuthorization(option =>
{
    option.AddPolicy("AnkaraPolicy", policy => policy.RequireClaim("city", "Ankara", "manisa"));
    option.AddPolicy("StudentPolicy", policy => policy.RequireClaim("rol", "öğrenci"));

});

//bir policy içinde birden fazla kueral olabilir,
//şehri ankara olanların erişebileceği bir policy tanımlamış olduk.

builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

builder.Services.ConfigureApplicationCookie(opt =>
    {
        var cookieBuilder = new CookieBuilder();
        cookieBuilder.Name = "duygucookie";
        opt.LoginPath = new PathString("/Home/SignIn");
        opt.LogoutPath = new PathString("/Member/logout");
        opt.Cookie = cookieBuilder;
        opt.ExpireTimeSpan = TimeSpan.FromDays(15);
        opt.SlidingExpiration = true;
        opt.AccessDeniedPath = new PathString("/Member/AccessDenied");
    }
);


var app = builder.Build();

//  Veritabanı Bağlantısını Test Et**
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    try
    {
        Console.WriteLine("Veritabanına bağlanmaya çalışılıyor...");

        if (dbContext.Database.CanConnect()) 
        {
            Console.WriteLine("Veritabanı bağlantısı başarılı!");
        }
        else
        {
            Console.WriteLine("Veritabanına bağlanılamadı!");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Veritabanı bağlantı hatası: {ex.Message}");
    }
}

//  Hata Yönetimi ve Güvenlik Ayarları**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error"); 
    app.UseHsts(); // HSTS güvenlik önlemini uygula
}

//  Middleware Konfigürasyonu**
app.UseHttpsRedirection(); // HTTP isteklerini HTTPS'e yönlendir
app.UseStaticFiles(); // **Statik dosyaları (CSS, JS, resimler) etkinleştir**
app.UseRouting(); // MVC yönlendirme mekanizmasını etkinleştir
app.UseAuthentication(); // **Kimlik doğrulamayı etkinleştir**
app.UseAuthorization(); // Yetkilendirme mekanizmasını etkinleştir


app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
).WithMetadata(new { Description = "Alan bazlı yönlendirme aktif!" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
    await RoleSeeder.SeedRoles(roleManager); // ✅ Doğru şekilde çağırıldı!
}

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await MenuSeeder.SeedMenus(context); // 📌 Menüleri veritabanına ekle
}


app.Run();

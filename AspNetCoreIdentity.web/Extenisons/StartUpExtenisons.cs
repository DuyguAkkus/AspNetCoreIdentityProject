using AspNetCoreIdentitiy.web.CustomValidations;
using AspNetCoreIdentitiy.web.Localizations;
using AspNetCoreIdentitiy.web.Models;


namespace AspNetCoreIdentitiy.web.Extenisons
{
    public static class StartUpExtenisons
    {
        public static void AddIdentityWithExt(this IServiceCollection services)
        {
            services.AddIdentity<AppUser, AppRole>(options =>
                {
                    options.User.RequireUniqueEmail = true;
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890_";

                    options.Password.RequiredLength = 6;
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireUppercase = true;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireDigit = false;
                })
                .AddPasswordValidator<PasswordValidator>()
                .AddUserValidator<UserValidator>()
                .AddErrorDescriber<LocalizationsIdentityErrorDescriber>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
    }
}
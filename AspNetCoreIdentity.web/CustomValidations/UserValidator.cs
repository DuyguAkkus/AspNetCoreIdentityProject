using Microsoft.AspNetCore.Identity;
using AspNetCoreIdentitiy.web.Models;

namespace AspNetCoreIdentitiy.web.CustomValidations
{
    public class UserValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var errors = new List<IdentityError>();

            // Kullanıcı adının ilk karakteri sayı içeremez
            bool isDigit = int.TryParse(user.UserName[0].ToString(), out _);
            if (isDigit)
            {
                errors.Add(new IdentityError
                {
                    Code = "UserNameContainFirstLetterDigit",
                    Description = "Kullanıcı adının ilk karakteri sayısal bir karakter içeremez"
                });
            }

            if (errors.Any())
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }

            return Task.FromResult(IdentityResult.Success);
        }
    }
}
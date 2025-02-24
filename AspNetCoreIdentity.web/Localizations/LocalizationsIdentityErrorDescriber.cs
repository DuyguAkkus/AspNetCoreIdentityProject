using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentitiy.web.Localizations
{
    public class LocalizationsIdentityErrorDescriber : IdentityErrorDescriber
    {
        // Kullanıcı adı zaten alınmışsa dönecek hata mesajı
        public override IdentityError DuplicateUserName(string userName)
        {
            return new IdentityError
            {
                Code = "DuplicateUserName",
                Description = $"Bu kullanıcı adı ({userName}) daha önce başka bir kullanıcı tarafından alınmıştır."
            };
        }

        // E-posta zaten alınmışsa dönecek hata mesajı
        public override IdentityError DuplicateEmail(string email)
        {
            return new IdentityError
            {
                Code = "DuplicateEmail",
                Description = $"Bu e-posta adresi ({email}) daha önce başka bir kullanıcı tarafından alınmıştır."
            };
        }

        // Şifre çok kısa ise dönecek hata mesajı
        public override IdentityError PasswordTooShort(int length)
        {
            return new IdentityError
            {
                Code = "PasswordTooShort",
                Description = $"Şifre en az {length} karakter uzunluğunda olmalıdır."
            };
        }

        // E-posta formatı hatalıysa dönecek hata mesajı
        public override IdentityError InvalidEmail(string email)
        {
            return new IdentityError
            {
                Code = "InvalidEmail",
                Description = $"Geçersiz e-posta adresi: ({email}). Lütfen doğru formatta bir e-posta adresi giriniz."
            };
        }
    }
}
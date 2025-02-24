using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentitiy.web.ViewModels
{
    public class SignInViewModel
    {
        // Varsayılan (Default) Constructor
        public SignInViewModel() { }

        // Parametreli Constructor
        public SignInViewModel(string email, string password)
        {
            Email = email;
            Password = password;
        }

        [Required(ErrorMessage = "E-posta alanı gereklidir.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Şifre alanı gereklidir.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string? Password { get; set; }
        public bool RememberMe { get; set; } // **Eksik olan özellik eklendi**
    }
}
using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentitiy.web.ViewModels
{
    public class SignUpViewModel
    {
        // Boş parametreli constructor
        public SignUpViewModel() { }

        // Parametreli constructor
        public SignUpViewModel(string userName, string email, string phone, string password, string confirmPassword)
        {
            UserName = userName;
            Email = email;
            Phone = phone;
            Password = password;
            ConfirmPassword = confirmPassword;
        }

        [Required]
        [Display(Name = "Kullanıcı Adı")]
        public required string UserName { get; set; }

        [Required]
        [Display(Name = "E-Posta Adresi")]
        public required string Email { get; set; }

        [Required]
        [Display(Name = "Telefon Numarası")]
        public required string Phone { get; set; }

        [Required]
        [Display(Name = "Şifre")]
        public required string Password { get; set; }

        [Required]
        [Display(Name = "Şifre Onayı")]
        public required string ConfirmPassword { get; set; }
    }
}
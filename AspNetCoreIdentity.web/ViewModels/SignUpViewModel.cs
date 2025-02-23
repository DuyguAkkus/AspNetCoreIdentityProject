using System.ComponentModel.DataAnnotations;

namespace AspNetCoreIdentitiy.web.ViewModels
{
    public class SignUpViewModel
    {
        // Boş parametreli constructor (ASP.NET Core Model Binding için gerekli)
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

        [Required(ErrorMessage = "Kullanıcı Adı Boş Bırakılamaz!")]
        [Display(Name = "Kullanıcı Adı")]
        public string UserName { get; set; } = string.Empty;

        [Required(ErrorMessage = "E-Posta adresi zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi girin.")]
        [Display(Name = "E-Posta Adresi")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Telefon Numarası Boş Bırakılamaz!")]
        [RegularExpression(@"^(\+90|0)?\d{10}$", ErrorMessage = "Geçerli bir telefon numarası girin.")]
        [Display(Name = "Telefon Numarası")]
        public string Phone { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre Alanı Boş Bırakılamaz!")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        [Display(Name = "Şifre")]
        public string Password { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre Tekrar Alanı Boş Bırakılamaz!")]
        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor.")]
        [Display(Name = "Şifre Onayı")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
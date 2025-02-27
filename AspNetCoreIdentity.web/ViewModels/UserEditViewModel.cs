using AspNetCoreIdentitiy.web.Models;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AspNetCoreIdentitiy.web.ViewModels
{
    public class UserEditViewModel
    {
        [Required(ErrorMessage = "Kullanıcı Ad alanı boş bırakılamaz.")]
        [Display(Name = "Kullanıcı Adı :")]
        public string UserName { get; set; } = null!;

        [EmailAddress(ErrorMessage = "Email formatı yanlıştır.")]
        [Required(ErrorMessage = "Email alanı boş bırakılamaz.")]
        [Display(Name = "Email :")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Telefon alanı boş bırakılamaz.")]
        [Display(Name = "Telefon :")]
        public string PhoneNumber { get; set; } = null!; // ✅ `Phone` yerine `PhoneNumber` kullandım, çünkü Identity’de bu şekilde geçiyor.

        [DataType(DataType.Date)]
        [Display(Name = "Doğum Tarihi :")]
        public DateTime? BirthDate { get; set; }

        [Display(Name = "Şehir :")]
        public string? City { get; set; }

        [Display(Name = "Profil Resmi :")]
        public IFormFile? ProfilePicture { get; set; } // ✅ `Picture` yerine daha anlamlı olması için `ProfilePicture` ismini verdim.

        [Display(Name = "Cinsiyet :")]
        public Gender? Gender { get; set; }
    }
}
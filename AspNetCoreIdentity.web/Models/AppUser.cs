using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentitiy.web.Models;

public class AppUser: IdentityUser
{
   public string City { get; set; } = string.Empty;
   public string Picture { get; set; } = string.Empty;
   public DateTime? Birthday { get; set; }
   public string Gender { get; set; } = string.Empty;
}
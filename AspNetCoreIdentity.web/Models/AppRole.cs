using Microsoft.AspNetCore.Identity;

namespace AspNetCoreIdentitiy.web.Models;

public class AppRole:IdentityRole

{
    public int AuthorityLevel { get; set; } // Yetkilendirme seviyesi (0-4)
}
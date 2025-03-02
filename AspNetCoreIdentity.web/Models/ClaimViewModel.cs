namespace AspNetCoreIdentitiy.web.Models;

public class ClaimViewModel
{
    public string Issuer { get; set; } = null!;
    public string ClaimType { get; set; } = null!;
    public string ClaimValue { get; set; } = null!;
}
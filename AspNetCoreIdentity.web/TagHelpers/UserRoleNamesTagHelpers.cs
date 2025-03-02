using System.Text;
using AspNetCoreIdentitiy.web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace AspNetCoreIdentitiy.web.TagHelpers
{
    [HtmlTargetElement("user-role-names", Attributes = "user-id")]
    public class UserRoleNamesTagHelper : TagHelper
    {
        public string UserId { get; set; } = null!;

        private readonly UserManager<AppUser> _userManager;

        public UserRoleNamesTagHelper(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            if (user == null)
            {
                output.Content.SetContent("Kullanıcı bulunamadı");
                return;
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            if (!userRoles.Any())
            {
                output.Content.SetContent("Rol atanmadı");
                return;
            }

            var stringBuilder = new StringBuilder();
            foreach (var role in userRoles)
            {
                stringBuilder.Append($"<span class='role-badge'>{role}</span>");
            }

            output.Content.SetHtmlContent(stringBuilder.ToString());
        }
    }
}
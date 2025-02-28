using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreIdentitiy.web.Extensions
{
    public static class ModelStateExtensions
    {
        // Liste olarak gelen hata mesajlarını ModelState'e ekler
        public static void AddModelErrorList(this ModelStateDictionary modelState, IEnumerable<string> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(string.Empty, error);
            }
        }

        // IdentityError listesi olarak gelen hata mesajlarını ModelState'e ekler
        public static void AddModelErrorList(this ModelStateDictionary modelState, IEnumerable<IdentityError> errors)
        {
            foreach (var error in errors)
            {
                modelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
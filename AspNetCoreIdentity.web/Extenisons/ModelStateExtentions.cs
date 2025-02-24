using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Collections.Generic;

namespace AspNetCoreIdentitiy.web.Extensions
{
    public static class ModelStateExtensions
    {
        public static void AddModelErrorList(this ModelStateDictionary modelState, List<string> errors)
        {
            errors.ForEach(error =>
            {
                modelState.AddModelError(string.Empty, error);
            });
        }
    }
}
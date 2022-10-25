using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent;

public static class EditContextExtensions
{
    /// <summary>
    /// Enables DataAnnotations validation support for the <see cref="EditContext"/>.
    /// </summary>
    /// <param name="editContext">The <see cref="EditContext"/>.</param>
    /// <param name="serviceProvider"></param>
    /// <param name="enableI18n"></param>
    public static void EnableValidation(this EditContext editContext, IServiceProvider serviceProvider, bool enableI18n)
    {
        new ValidationEventSubscriptions(editContext, serviceProvider, enableI18n);
    }
}

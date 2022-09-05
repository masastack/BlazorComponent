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
    /// <returns>A disposable object whose disposal will remove DataAnnotations validation support from the <see cref="EditContext"/>.</returns>
    public static IDisposable EnableValidation(this EditContext editContext, IServiceProvider serviceProvider, bool enableI18n)
    {
        return new ValidationEventSubscriptions(editContext, serviceProvider, enableI18n);
    }
}

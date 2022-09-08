using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent;

public class FormContext
{
    public EditContext EditContext { get; }

    public Func<Task<bool>> ValidateAsync { get; }

    public FormContext(EditContext editContext, Func<Task<bool>> validateAsync)
    {
        EditContext = editContext;
        ValidateAsync = validateAsync;
    }
}

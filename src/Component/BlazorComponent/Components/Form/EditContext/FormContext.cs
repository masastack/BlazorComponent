using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent;

public class FormContext
{
    public EditContext EditContext { get; }

    public Func<bool> Validate { get; }

    public FormContext(EditContext editContext, Func<bool> validate)
    {
        EditContext = editContext;
        Validate = validate;
    }
}

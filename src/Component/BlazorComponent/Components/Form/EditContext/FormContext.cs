using BlazorComponent.Form;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent;

public class FormContext
{
    public EditContext EditContext { get; }

    public BForm Form { get; }

    public FormContext(EditContext editContext, BForm form)
    {
        EditContext = editContext;
        Form = form;
    }

    public bool Validate() => Form.Validate();

    public void ParseFormValidation(string validationMessage) => Form.ParseFormValidation(validationMessage);

    public bool TryParseFormValidation(string validationMessage) => Form.TryParseFormValidation(validationMessage);

    public void ParseFormValidation(List<ValidationResult> validationResults) => Form.ParseFormValidation(validationResults);
}

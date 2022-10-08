using BlazorComponent.Form;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent;

public class FormContext
{
    public EditContext EditContext { get; }

    private BForm Form { get; }

    public FormContext(EditContext editContext, BForm form)
    {
        EditContext = editContext;
        Form = form;
    }

    public bool Validate() => Form.Validate();

    /// <summary>
    /// parse form validation result,if parse faield throw exception
    /// </summary>
    /// <param name="validationResult">
    /// validation result
    /// see deatils https://blazor.masastack.com/components/forms
    /// </param>
    public void ParseFormValidation(string validationResult) => Form.ParseFormValidation(validationResult);

    /// <summary>
    /// parse form validation result,if parse faield return false
    /// </summary>
    /// <param name="validationResult">
    /// validation result
    /// see deatils https://blazor.masastack.com/components/forms
    /// </param>
    /// <returns></returns>
    public bool TryParseFormValidation(string validationResult) => Form.TryParseFormValidation(validationResult);

    public void ParseFormValidation(IEnumerable<ValidationResult> validationResults) => Form.ParseFormValidation(validationResults);
}

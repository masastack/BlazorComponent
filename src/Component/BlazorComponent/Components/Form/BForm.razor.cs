using BlazorComponent.Form;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent
{
    public partial class BForm : BDomComponentBase
    {
        [Inject]
        public IServiceProvider ServiceProvider { get; set; }

        [Parameter]
        public RenderFragment<FormContext> ChildContent { get; set; }

        [Parameter]
        public EventCallback<EventArgs> OnSubmit { get; set; }

        [Parameter]
        public object Model { get; set; }

        [Parameter]
        public bool EnableValidation { get; set; }

        [Parameter]
        public bool EnableI18n { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Readonly { get; set; }

        [Parameter]
        public bool Value { get; set; }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public EventCallback OnValidSubmit { get; set; }

        [Parameter]
        public EventCallback OnInvalidSubmit { get; set; }

        private object _oldModel;

        public EditContext EditContext { get; protected set; }

        protected List<IValidatable> Validatables { get; } = new();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (_oldModel != Model)
            {
                EditContext = new EditContext(Model);

                if (EnableValidation)
                {
                    EditContext.EnableValidation(ServiceProvider, EnableI18n);
                }

                _oldModel = Model;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            var hasError = Validatables.Any(v => v.HasError);
            if (Value != !hasError && ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(!hasError);
            }
        }

        public void Register(IValidatable validatable)
        {
            Validatables.Add(validatable);
        }

        private async Task HandleOnSubmitAsync(EventArgs args)
        {
            var valid = Validate();

            if (OnSubmit.HasDelegate)
            {
                await OnSubmit.InvokeAsync(args);
            }

            if (valid)
            {
                if (OnValidSubmit.HasDelegate)
                {
                    await OnValidSubmit.InvokeAsync();
                }
            }
            else
            {
                if (OnInvalidSubmit.HasDelegate)
                {
                    await OnInvalidSubmit.InvokeAsync();
                }
            }
        }

        public bool Validate()
        {
            var valid = true;

            foreach (var validatable in Validatables)
            {
                var success = validatable.Validate();
                if (!success)
                {
                    valid = false;
                }
            }

            if (EditContext != null)
            {
                if (valid is true) valid = EditContext.Validate();
            }

            if (ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(valid);
            }

            return valid;
        }

        public void ParseFormValidation(string validationMessage)
        {
            if (TryParseFormValidation(validationMessage) is false)
                throw new Exception(validationMessage);
        }

        public bool TryParseFormValidation(string validationMessage)
        {
            if (string.IsNullOrEmpty(validationMessage)) return false;
            var resultStrs = validationMessage.Split("\r\n").ToList();
            if (resultStrs.Count < 1 || resultStrs[0].StartsWith("Validation failed:") is false) return false;
            resultStrs.RemoveAt(0);
            var validationResults = new List<ValidationResult>();
            foreach (var resultStr in resultStrs)
            {
                int startIndex = resultStr.IndexOf(" -- ") + 4;
                if (startIndex < 4) continue;
                int colonIndex = resultStr.IndexOf(": ");
                var field = resultStr.Substring(startIndex, colonIndex - startIndex);
                int severityIndex = resultStr.IndexOf("Severity: ");
                colonIndex += 2;
                var msg = resultStr.Substring(colonIndex, severityIndex - colonIndex);
                Enum.TryParse<ValidationResultTypes>(resultStr.Substring(severityIndex + 10), out var type);
                validationResults.Add(new ValidationResult
                {
                    Message = msg,
                    Field = field,
                    ValidationResultType = type
                });
            }
            ParseFormValidation(validationResults);

            return true;
        }

        public void ParseFormValidation(List<ValidationResult> validationResults)
        {
            var messageStore = new ValidationMessageStore(EditContext);
            foreach (var validationResult in validationResults.Where(item => item.ValidationResultType == ValidationResultTypes.Error))
            {
                var field = new FieldIdentifier(Model, validationResult.Field);
                var validatable = Validatables.FirstOrDefault(item => item.ValueIdentifier.Equals(field));
                if (validatable is not null)
                {
                    validatable.Validate();
                    messageStore.Add(field, validationResult.Message);
                }
            }
            EditContext.NotifyValidationStateChanged();
            if (ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(false);
            }
            else Value = false;
        }

        public void Reset()
        {
            EditContext?.MarkAsUnmodified();

            foreach (var validatable in Validatables)
            {
                validatable.Reset();
            }

            if (ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(true);
            }
        }

        public void ResetValidation()
        {
            EditContext?.MarkAsUnmodified();

            foreach (var validatable in Validatables)
            {
                validatable.ResetValidation();
            }

            if (ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(true);
            }
        }
    }
}

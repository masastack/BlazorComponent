using BlazorComponent.Form;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;
using System.Reflection;

namespace BlazorComponent
{
    public partial class BForm : BDomComponentBase
    {
        [Inject]
        public IServiceProvider ServiceProvider { get; set; } = null!;

        [Parameter]
        public RenderFragment<FormContext?>? ChildContent { get; set; }

        [Parameter]
        public EventCallback<EventArgs> OnSubmit { get; set; }

        [Parameter]
        public object? Model { get; set; }

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

        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> s_modelPropertiesMap = new();

        private object? _oldModel;
        private IDisposable? _editContextValidation;

        public EditContext? EditContext { get; protected set; }

        public FormContext? FormContext { get; protected set; }

        protected ValidationMessageStore? ValidationMessageStore { get; set; }

        protected List<IValidatable> Validatables { get; } = new();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Model != null && _oldModel != Model)
            {
                EditContext = new EditContext(Model);
                FormContext = new FormContext(EditContext, this);

                ValidationMessageStore = new ValidationMessageStore(EditContext);
                if (EnableValidation)
                {
                    _editContextValidation = EditContext.EnableValidation(ValidationMessageStore, ServiceProvider, EnableI18n);
                }

                _oldModel = Model;
            }
        }

        internal void UpdateValidValue()
        {
            var hasError = Validatables.Any(v =>  v.HasError);
            var valid = !hasError;
            _ = UpdateValue(valid);
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
                var success = EditContext.Validate();

                valid = valid && success;
            }

            if (ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(valid);
            }

            return valid;
        }

        /// <summary>
        /// parse form validation result,if parse faield throw exception
        /// </summary>
        /// <param name="validationResult">
        /// validation result
        /// see deatils https://blazor.masastack.com/components/forms
        /// </param>
        public void ParseFormValidation(string validationResult)
        {
            if (TryParseFormValidation(validationResult) is false)
                throw new Exception(validationResult);
        }

        /// <summary>
        /// parse form validation result,if parse failed return false
        /// </summary>
        /// <param name="validationResult">
        /// validation result
        /// see deatils https://blazor.masastack.com/components/forms
        /// </param>
        /// <returns></returns>
        public bool TryParseFormValidation(string validationResult)
        {
            if (string.IsNullOrEmpty(validationResult)) return false;
            var resultStrs = validationResult.Split(Environment.NewLine).ToList();
            if (resultStrs.Count < 1 || resultStrs[0].StartsWith("Validation failed:") is false) return false;
            resultStrs.RemoveAt(0);
            var validationResults = new List<ValidationResult>();
            foreach (var resultStr in resultStrs)
            {
                int startIndex = resultStr.IndexOf(" -- ", StringComparison.Ordinal) + 4;
                if (startIndex < 4) continue;
                int colonIndex = resultStr.IndexOf(": ", StringComparison.Ordinal);
                var field = resultStr.Substring(startIndex, colonIndex - startIndex);
                int severityIndex = resultStr.IndexOf("Severity: ", StringComparison.Ordinal);
                colonIndex += 2;
                var msg = resultStr.Substring(colonIndex, severityIndex - colonIndex);
                Enum.TryParse<ValidationResultTypes>(resultStr.Substring(severityIndex + 10), out var type);
                validationResults.Add(new ValidationResult(field, msg, type));
            }

            ParseFormValidation(validationResults.ToArray());

            return true;
        }

        public void ParseFormValidation(IEnumerable<ValidationResult> validationResults)
        {
            if (Model == null) return;

            foreach (var validationResult in validationResults.Where(item => item.ValidationResultType == ValidationResultTypes.Error))
            {
                var model = Model;
                var field = validationResult.Field;
                if (validationResult.Field?.Contains('.') is true)
                {
                    var fieldChunks = validationResult.Field.Split('.');
                    field = fieldChunks.Last();
                    foreach (var fieldChunk in fieldChunks)
                    {
                        if (fieldChunk != field)
                            model = GetModelValue(model!, fieldChunk,
                                () => throw new Exception($"{validationResult.Field} is error,can not read {fieldChunk}"));
                    }
                }

                if (model is null) return;

                var fieldIdentifier = new FieldIdentifier(model, field);
                var validatable = Validatables.FirstOrDefault(item => item.ValueIdentifier.Equals(fieldIdentifier));
                if (validatable is not null)
                {
                    ValidationMessageStore?.Clear(fieldIdentifier);
                    ValidationMessageStore?.Add(fieldIdentifier, validationResult.Message);
                }
            }

            EditContext?.NotifyValidationStateChanged();

            _ = UpdateValue(false);

            object? GetModelValue(object model, string fieldChunk, Action whenError)
            {
                var type = model.GetType();
                if (s_modelPropertiesMap.TryGetValue(type, out var propertyInfos) is false)
                {
                    propertyInfos = type.GetProperties();
                    s_modelPropertiesMap[type] = propertyInfos;
                }

                if (fieldChunk.Contains('['))
                {
                    var leftBracketsIndex = fieldChunk.IndexOf('[') + 1;
                    var rightBracketsIndex = fieldChunk.IndexOf(']');
                    var filedName = fieldChunk.Substring(0, leftBracketsIndex - 1);
                    var propertyInfo = propertyInfos.FirstOrDefault(item => item.Name == filedName);
                    if (propertyInfo is null)
                    {
                        whenError.Invoke();
                    }
                    else
                    {
                        model = propertyInfo.GetValue(model);
                        var enumerable = model as System.Collections.IEnumerable;
                        var index = Convert.ToInt32(fieldChunk.Substring(leftBracketsIndex, rightBracketsIndex - leftBracketsIndex));
                        var i = 0;
                        foreach (var item in enumerable)
                        {
                            if (i == index)
                            {
                                model = item;
                                break;
                            }

                            i++;
                        }
                    }
                }
                else
                {
                    var propertyInfo = propertyInfos.FirstOrDefault(item => item.Name == fieldChunk);
                    if (propertyInfo is null)
                    {
                        whenError.Invoke();
                    }
                    else
                    {
                        model = propertyInfo.GetValue(model);
                    }
                }

                return model;
            }
        }

        public void Reset()
        {
            EditContext?.MarkAsUnmodified();

            foreach (var validatable in Validatables)
            {
                validatable.Reset();
            }

            _ = UpdateValue(true);
        }

        public void ResetValidation()
        {
            EditContext?.MarkAsUnmodified();

            foreach (var validatable in Validatables)
            {
                validatable.ResetValidation();
            }

            _ = UpdateValue(true);
        }

        private async Task UpdateValue(bool val)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(val);
            }
            else
            {
                Value = val;
            }
        }

        protected override async ValueTask DisposeAsync(bool disposing)
        {
            _editContextValidation?.Dispose();

            await base.DisposeAsync(disposing);
        }
    }
}

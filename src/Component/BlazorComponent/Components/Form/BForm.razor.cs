using BlazorComponent.Form;
using Microsoft.AspNetCore.Components.Forms;
using System.Collections.Concurrent;
using System.Reflection;

namespace BlazorComponent
{
    public partial class BForm : BDomComponentBase
    {
        private static readonly ConcurrentDictionary<Type, PropertyInfo[]> _modelPropertiesMap = new();

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
        /// parse form validation result,if parse faield return false
        /// </summary>
        /// <param name="validationResult">
        /// validation result
        /// see deatils https://blazor.masastack.com/components/forms
        /// </param>
        /// <returns></returns>
        public bool TryParseFormValidation(string validationResult)
        {
            Console.ResetColor();
            Console.BackgroundColor = ConsoleColor.Red;

            Console.WriteLine(validationResult);

            Console.WriteLine("-------- step 2 -----------");
            if (string.IsNullOrEmpty(validationResult)) return false;
            Console.WriteLine("-------- step 3 -----------");
            var resultStrs = validationResult.Split("\r\n").ToList();
            if (resultStrs.Count < 1 || resultStrs[0].StartsWith("Validation failed:") is false) return false;
            Console.WriteLine("-------- step 4 -----------");
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
            Console.WriteLine("-------- step 5 -----------");
            ParseFormValidation(validationResults.ToArray());

            return true;
        }

        public void ParseFormValidation(IEnumerable<ValidationResult> validationResults)
        {
            var messageStore = new ValidationMessageStore(EditContext);
            foreach (var validationResult in validationResults.Where(item => item.ValidationResultType == ValidationResultTypes.Error))
            {
                var model = Model;
                var field = validationResult.Field;
                if (validationResult.Field.Contains('.'))
                {
                    var fieldChunks = validationResult.Field.Split('.');
                    field = fieldChunks.Last();
                    foreach (var fieldChunk in fieldChunks)
                    {
                        if(fieldChunk != field)
                            model = GetModelValue(model, fieldChunk, () => throw new Exception($"{validationResult.Field} is error,can not read {fieldChunk}"));
                    }                
                }
                var fieldIdentifuer = new FieldIdentifier(model, field);
                var validatable = Validatables.FirstOrDefault(item => item.ValueIdentifier.Equals(fieldIdentifuer));
                if (validatable is not null)
                {
                    validatable.Validate();
                    messageStore.Add(fieldIdentifuer, validationResult.Message);
                }
            }
            Console.WriteLine("-------- step 6 -----------");
            EditContext.NotifyValidationStateChanged();
            if (ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(false);
            }
            else Value = false;

            object GetModelValue(object model, string fieldChunk, Action whenError)
            {
                var type = model.GetType();
                if (_modelPropertiesMap.TryGetValue(type, out var propertyInfos) is false)
                {
                    propertyInfos = type.GetProperties();
                    _modelPropertiesMap[type] = propertyInfos;
                }
                if (fieldChunk.Contains('['))
                {
                    var leftBracketsIndex = fieldChunk.IndexOf('[') + 1;
                    var rightBracketsIndex = fieldChunk.IndexOf(']');
                    var filedName = fieldChunk.Substring(0, leftBracketsIndex - 1);
                    var propertyInfo = propertyInfos.FirstOrDefault(item => item.Name == filedName);
                    if (propertyInfo is null) whenError.Invoke();
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
                else
                {
                    var propertyInfo = propertyInfos.FirstOrDefault(item => item.Name == fieldChunk);
                    if (propertyInfo is null) whenError.Invoke();
                    model = propertyInfo.GetValue(model);
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

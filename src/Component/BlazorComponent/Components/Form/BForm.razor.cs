using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace BlazorComponent
{
    public partial class BForm : BDomComponentBase
    {
        private object _oldModel;

        [Inject]
        public IServiceProvider ServiceProvider { get;set; }

        [Parameter]
        public RenderFragment<EditContext> ChildContent { get; set; }

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

        public EditContext EditContext { get; protected set; }

        protected List<IValidatable> Validatables { get; } = new List<IValidatable>();

        protected override void OnParametersSet()
        {
            if (_oldModel != Model)
            {
                //EditContext changed,re-subscribe OnValidationStateChanged event
                if (EditContext != null)
                {
                    EditContext.OnValidationStateChanged -= OnValidationStateChanged;
                }

                EditContext = new EditContext(Model);
                EditContext.OnValidationStateChanged += OnValidationStateChanged;

                if (EnableValidation)
                {
                    EditContext.EnableValidation(ServiceProvider, EnableI18n);
                }

                _oldModel = Model;
            }
        }

        private void OnValidationStateChanged(object sender, ValidationStateChangedEventArgs e)
        {
            var value = !EditContext.GetValidationMessages().Any();
            if (value != Value && ValueChanged.HasDelegate)
            {
                _ = ValueChanged.InvokeAsync(value);
            }
        }

        public void Register(IValidatable validatable)
        {
            Validatables.Add(validatable);
        }

        private async Task HandleOnSubmitAsync(EventArgs args)
        {
            var valid = await ValidateAsync();

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

        public async Task<bool> ValidateAsync()
        {
            //REVIEW: We should combine this
            var valid = true;
            if (EditContext != null)
            {
                valid = EditContext.Validate();
            }
            else
            {
                foreach (var validatable in Validatables)
                {
                    var success = await validatable.ValidateAsync();
                    if (!success)
                    {
                        valid = false;
                    }
                }
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(valid);
            }

            return valid;
        }

        public async Task ResetAsync()
        {
            if (EditContext != null)
            {
                EditContext.MarkAsUnmodified();
            }

            foreach (var validatable in Validatables)
            {
                await validatable.ResetAsync();
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(true);
            }
        }

        public async Task ResetValidationAsync()
        {
            if (EditContext != null)
            {
                EditContext.MarkAsUnmodified();
            }

            foreach (var validatable in Validatables)
            {
                await validatable.ResetValidationAsync();
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(true);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (EditContext != null)
            {
                EditContext.OnValidationStateChanged -= OnValidationStateChanged;
            }

            base.Dispose(disposing);
        }
    }
}

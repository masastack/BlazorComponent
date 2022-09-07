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
            var valid = true;

            foreach (var validatable in Validatables)
            {
                var success = await validatable.ValidateAsync();
                if (!success)
                {
                    valid = false;
                }
            }

            if (EditContext != null)
            {
                valid = EditContext.Validate();
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(valid);
            }

            return valid;
        }

        public async Task ResetAsync()
        {
            EditContext?.MarkAsUnmodified();

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
            EditContext?.MarkAsUnmodified();

            foreach (var validatable in Validatables)
            {
                await validatable.ResetValidationAsync();
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(true);
            }
        }
    }
}

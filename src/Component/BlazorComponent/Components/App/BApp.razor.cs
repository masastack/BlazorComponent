using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public abstract partial class BApp : BDomComponentBase, IThemeable
    {
        [Inject]
        private IPopupProvider PopupProvider { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        protected override void OnInitialized()
        {
            PopupProvider.StateChanged += OnStateChanged;

            base.OnInitialized();
        }

        public bool IsDark
        {
            get
            {
                if (Dark)
                {
                    return true;
                }

                if (Light)
                {
                    return false;
                }

                return CascadingIsDark;
            }
        }


        private void OnStateChanged(object? sender, EventArgs e)
        {
            InvokeAsync(() => StateHasChanged());
        }


        public override void Dispose()
        {
            PopupProvider.StateChanged -= OnStateChanged;
            base.Dispose();
        }
    }
}
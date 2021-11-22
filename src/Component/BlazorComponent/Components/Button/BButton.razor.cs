using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public abstract partial class BButton : BGroupItem<ItemGroupBase>, IThemeable, IButton, IRoutable
    {
        protected BButton() : base(GroupType.ButtonGroup)
        {
        }

        /// <summary>
        /// Determine whether rendering a loader component
        /// </summary>
        protected bool HasLoader { get; set; }

        /// <summary>
        /// Set the button's type attribute
        /// </summary>
        protected string TypeAttribute { get; set; } = "button";

        [Parameter]
        public bool Block { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public StringNumber Height { get; set; }

        [Parameter]
        public RenderFragment LoaderContent { get; set; }

        [Parameter]
        public virtual bool Loading { get; set; }

        [Parameter]
        public StringNumber MaxHeight { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public StringNumber MinHeight { get; set; }

        [Parameter]
        public StringNumber MinWidth { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        [Parameter]
        public string Href { get; set; }

        [Parameter]
        public bool Link { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public string Tag { get; set; } = "button";

        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public bool StopPropagation { get; set; }

        public virtual bool IsDark { get; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            IRoutable router = new Router(this);

            (Tag, Attributes) = router.GenerateRouteLink();
        }

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            await OnClick.InvokeAsync(args);

            await ToggleItem();
        }
    }
}
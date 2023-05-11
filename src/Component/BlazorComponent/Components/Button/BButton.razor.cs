using Microsoft.AspNetCore.Components.Routing;

namespace BlazorComponent
{
    public partial class BButton : BRoutableGroupItem<ItemGroupBase>, IThemeable, IButton
    {
        protected BButton() : base(GroupType.ButtonGroup, "button")
        {
        }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

        [Parameter]
        public bool Block { get; set; }

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public StringNumber? Height { get; set; }

        [Parameter]
        public RenderFragment? LoaderContent { get; set; }

        [Parameter]
        public virtual bool Loading { get; set; }

        [Parameter]
        public StringNumber? MaxHeight { get; set; }

        [Parameter]
        public StringNumber? MaxWidth { get; set; }

        [Parameter]
        public StringNumber? MinHeight { get; set; }

        [Parameter]
        public StringNumber? MinWidth { get; set; }

        [Parameter]
        public bool Outlined { get; set; }

        [Parameter]
        public StringNumber? Width { get; set; }

        [Parameter]
        public bool OnClickStopPropagation { get; set; }

        [Parameter]
        public bool OnClickPreventDefault { get; set; }

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [Parameter]
        public bool? Show { get; set; }

        [Parameter]
        public string? Key { get; set; }

        /// <summary>
        /// Determine whether rendering a loader component
        /// </summary>
        protected bool HasLoader { get; set; }

        /// <summary>
        /// Set the button's type attribute
        /// </summary>
        protected string TypeAttribute { get; set; } = "button";

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

        protected override bool AfterHandleEventShouldRender() => false;

        protected virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }

            await ToggleAsync();
        }
    }
}

using BlazorComponent.Helpers;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BIcon : IIcon, IThemeable
    {
        [Parameter]
        [EditorRequired]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool If { get; set; } = true;

        #region IIcon

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public StringNumber? Size { get; set; }

        [Parameter]
        public string Tag { get; set; } = "i";

        #endregion

        [Parameter]
        public bool Dark { get; set; }

        [Parameter]
        public bool Light { get; set; }

        [CascadingParameter(Name = "IsDark")]
        public bool CascadingIsDark { get; set; }

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

        [Parameter]
        public string? Color { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        [Parameter]
        public bool OnClickPreventDefault { get; set; }

        [Parameter]
        public bool OnClickStopPropagation { get; set; }

        [Inject]
        [NotNull]
        public Document? Document { get; set; }

        private bool _clickEventRegistered;

        protected string? Icon { get; set; }

        protected IconType IconType { get; set; }

        protected Dictionary<string, object>? SvgAttrs { get; set; }

        protected override void OnInitialized()
        {
            base.OnInitialized();

            SvgAttrs = new()
            {
                { "viewBox", "0 0 24 24" },
                { "role", "img" },
                { "aria-hidden", "true" }
            };
        }

        public override async Task SetParametersAsync(ParameterView parameters)
        {
            await base.SetParametersAsync(parameters);

            InitIcon();
        }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (OnClick.HasDelegate)
            {
                Tag = "button";
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (!_clickEventRegistered)
            {
                await TryRegisterClickEvent();
            }
        }

        protected override async Task OnElementReferenceChangedAsync()
        {
            await TryRegisterClickEvent();
        }

        public virtual async Task HandleOnClick(MouseEventArgs args)
        {
            if (OnClick.HasDelegate)
            {
                await OnClick.InvokeAsync(args);
            }
        }

        private async Task TryRegisterClickEvent()
        {
            if (Ref.Context is not null && OnClick.HasDelegate)
            {
                _clickEventRegistered = true;

                var button = Document.GetElementByReference(Ref);
                await button.AddEventListenerAsync("click", CreateEventCallback<MouseEventArgs>(HandleOnClick), false, new EventListenerExtras
                {
                    PreventDefault = OnClickPreventDefault,
                    StopPropagation = OnClickStopPropagation
                });
            }
        }

        private void InitIcon()
        {
            var builder = new RenderTreeBuilder();
            ChildContent?.Invoke(builder);

#pragma warning disable BL0006 // Do not use RenderTree types

            var frame = builder.GetFrames().Array
                               .FirstOrDefault(u => u.FrameType is RenderTreeFrameType.Text or RenderTreeFrameType.Markup);

            char[] charsToTrim = { '\r', ' ', '\n' };
            var textContent = frame.TextContent.Trim(charsToTrim);

            if (RegexHelper.RegexSvgPath(textContent))
            {
                IconType = IconType.Svg;
            }
            else if (textContent.IndexOf("-", StringComparison.Ordinal) < 0)
            {
                IconType = IconType.WebfontNoPseudo;
            }
            else
            {
                IconType = IconType.Webfont;
            }

            Icon = textContent;

#pragma warning restore BL0006 // Do not use RenderTree types
        }
    }
}

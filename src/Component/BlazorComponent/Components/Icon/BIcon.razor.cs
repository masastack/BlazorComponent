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
        [Inject]
        public Document? Document { get; set; }
        
        [Parameter]
        [EditorRequired]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public bool If { get; set; } = true;

        [Parameter]
        public bool Dense { get; set; }

        [Parameter]
        public bool Disabled { get; set; }

        [Parameter]
        public Icon? Icon { get; set; }

        [Parameter]
        public bool Left { get; set; }

        [Parameter]
        public bool Right { get; set; }

        [Parameter]
        public StringNumber? Size { get; set; }

        [Parameter]
        public string Tag { get; set; } = "i";

        [Parameter]
        public Dictionary<string, object?>? SvgAttributes { get; set; }

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

        [Parameter]
        public bool OnMouseupPreventDefault { get; set; }

        [Parameter]
        public bool OnMouseupStopPropagation { get; set; }

        private readonly static Dictionary<string, object?> s_defaultSvgAttrs = new()
        {
            { "viewBox", "0 0 24 24" },
            { "role", "img" },
            { "aria-hidden", "true" }
        }; 

        private bool _clickEventRegistered;

        /// <summary>
        /// Icon from ChildContent
        /// </summary>
        protected string? IconContent { get; set; }

        protected virtual Icon? ComputedIcon { get; set; }

        private Dictionary<string, object?> SvgAttrs
        {
            get
            {
                if (SvgAttributes is null) return s_defaultSvgAttrs;

                var attrs = new Dictionary<string, object?>(SvgAttributes);

                foreach (var (k, v) in s_defaultSvgAttrs)
                {
                    attrs.TryAdd(k, v);
                }

                return attrs;
            }
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

        protected virtual void InitIcon()
        {
            if (Icon is not null)
            {
                ComputedIcon = Icon;
            }
            else
            {
                var textContent = ChildContent?.GetTextContent();
                IconContent = textContent;

                if (!string.IsNullOrWhiteSpace(textContent) && CheckIfSvg(textContent))
                {
                    ComputedIcon = new SvgPath(textContent);
                }
                else
                {
                    ComputedIcon = textContent;
                }
            }
        }

        protected static bool CheckIfSvg(string iconOrPath)
        {
            return RegexHelper.RegexSvgPath(iconOrPath);
        }
    }
}

using BlazorComponent.JSInterop;
using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract partial class BNavigationDrawer : BDomComponentBase, IDependent
    {
        private CancellationTokenSource _cancellationTokenSource;
        private bool _disposed;
        private readonly List<IDependent> _dependents = new();

        [Parameter]
        public bool ExpandOnHover
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool MiniVariant { get; set; }

        [Parameter]
        public EventCallback<bool> MiniVariantChanged { get; set; }

        [Parameter]
        public bool Permanent { get; set; }

        [Parameter]
        public string Src { get; set; }

        [Parameter]
        public bool Stateless { get; set; }

        [Parameter]
        public string Tag
        {
            get
            {
                return GetValue(App ? "nav" : "aside");
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public bool Temporary { get; set; }

        [Parameter]
        public bool Value
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        [Parameter]
        public EventCallback<bool> ValueChanged { get; set; }

        [Parameter]
        public bool App { get; set; }

        [Parameter]
        public bool HideOverlay { get; set; }

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
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public RenderFragment<Dictionary<string, object>> ImgContent { get; set; }

        [Inject]
        private Document Document { get; set; }

        protected object Overlay { get; set; }

        protected ElementReference? OverlayRef => ((BOverlay)Overlay)?.Ref;

        protected bool IsMouseover
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        //TODO: TouchArea,StackMinZIndex

        protected virtual bool IsMobileBreakpoint { get; }

        protected bool IsActive
        {
            get
            {
                return GetValue<bool>();
            }
            set
            {
                SetValue(value);
            }
        }

        protected bool IsMobile => !Stateless && !Permanent && IsMobileBreakpoint;//TODO: fix mobile

        protected bool ReactsToClick
        {
            get
            {
                return !Stateless && !Permanent && (IsMobile || Temporary);
            }
        }

        protected bool ShowOverlay
        {
            get
            {
                return (!HideOverlay && IsActive && (IsMobile || Temporary));
            }
        }

        public IEnumerable<HtmlElement> DependentElements
        {
            get
            {
                var elements = _dependents
                    .SelectMany(dependent => dependent.DependentElements)
                    .ToList();

                var element = Document.GetElementByReference(Ref);
                elements.Add(element);

                return elements;
            }
        }

        public void RegisterChild(IDependent dependent)
        {
            _dependents.Add(dependent);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await Js.AddOutsideClickEventListener(HandleOnOutsideClickAsync,DependentElements.Select(element => element.Selector));

                if (!Permanent && !Stateless && !Temporary)
                {
                    await UpdateValue(!IsMobile);
                }
            }
        }

        public virtual Task HandleOnClickAsync(MouseEventArgs e)
        {
            return Task.CompletedTask;
        }

        public virtual async Task HandleOnMouseEnterAsync(MouseEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Delay(150, _cancellationTokenSource.Token);

            if (ExpandOnHover)
            {
                IsMouseover = true;
            }
        }

        public virtual async Task HandleOnMouseLeaveAsync(MouseEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await Task.Delay(150, _cancellationTokenSource.Token);

            if (ExpandOnHover)
            {
                IsMouseover = false;
            }
        }

        //TODO ontransitionend事件

        private async Task HandleOnOutsideClickAsync(object _)
        {
            if (!CloseConditional()) return;
            await UpdateValue(false);
        }

        protected virtual bool IsFullscreen => false;

        protected async Task HideScroll()
        {
            await JsInvokeAsync(JsInteropConstants.HideScroll, IsFullscreen, OverlayRef.GetSelector());
        }

        protected async Task ShowScroll()
        {
            await JsInvokeAsync(JsInteropConstants.ShowScroll, OverlayRef.GetSelector());
        }

        protected bool CloseConditional()
        {
            return IsActive && !_disposed && ReactsToClick;
        }

        protected async Task UpdateValue(bool value)
        {
            if (Value == value)
            {
                return;
            }

            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(value);
            }
            else
            {
                Value = value;
                StateHasChanged();
            }
        }

        protected override void Dispose(bool disposing)
        {
            _disposed = true;
        }
    }
}

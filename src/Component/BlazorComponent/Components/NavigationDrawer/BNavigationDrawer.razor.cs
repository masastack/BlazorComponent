using BlazorComponent.JSInterop;
using BlazorComponent.Mixins;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public abstract partial class BNavigationDrawer : BDomComponentBase, IDependent, IOutsideClickJsCallback
    {
        private CancellationTokenSource? _cancellationTokenSource;
        private bool _disposed;

        [Inject]
        private OutsideClickJSModule? OutsideClickJsModule { get; set; }
        
        [CascadingParameter]
        public IDependent? CascadingDependent { get; set; }

        [Parameter]
        public bool ExpandOnHover
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        [Parameter]
        public bool MiniVariant
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        [Parameter]
        public EventCallback<bool> MiniVariantChanged { get; set; }

        [Parameter]
        public bool Permanent { get; set; }

        [Parameter]
        public string? Src { get; set; }

        [Parameter]
        public bool Stateless { get; set; }

        [Parameter]
        public string Tag
        {
            get => GetValue(App ? "nav" : "aside")!;
            set => SetValue(value);
        }

        [Parameter]
        public bool Temporary { get; set; }

        [Parameter]
        public bool? Value
        {
            get => GetValue<bool?>();
            set => SetValue(value);
        }

        [Parameter]
        public EventCallback<bool?> ValueChanged { get; set; }

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
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        public RenderFragment<Dictionary<string, object?>>? ImgContent { get; set; }

        private readonly List<IDependent> _dependents = new();

        protected object? Overlay { get; set; }

        protected ElementReference? OverlayRef => (Overlay as BOverlay)?.Ref;

        protected bool IsMouseover
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        //TODO: TouchArea,StackMinZIndex

        protected virtual bool IsMobileBreakpoint { get; }

        protected bool IsActive
        {
            get => GetValue<bool>();
            set => SetValue(value);
        }

        protected bool IsMobile => !Stateless && !Permanent && IsMobileBreakpoint; //TODO: fix mobile

        protected bool ReactsToClick => !Stateless && !Permanent && (IsMobile || Temporary);

        protected bool ShowOverlay => !HideOverlay && IsActive && (IsMobile || Temporary);

        public virtual IEnumerable<string> DependentSelectors
        {
            get
            {
                var elements = _dependents.SelectMany(dependent => dependent.DependentSelectors).ToList();

                // do not use the Ref elementReference because it's delay assignment.
                elements.Add($"#{Id}");

                return elements;
            }
        }

        public void RegisterChild(IDependent dependent)
        {
            _dependents.Add(dependent);

            NextTickWhile(() => { OutsideClickJsModule?.UpdateDependentElements(DependentSelectors.ToArray()); },
                () => OutsideClickJsModule == null || OutsideClickJsModule.Initialized == false);
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                await OutsideClickJsModule!.InitializeAsync(this, DependentSelectors.ToArray());
            }
        }

        protected override void RegisterWatchers(PropertyWatcher watcher)
        {
            base.RegisterWatchers(watcher);

            watcher.Watch<bool>(nameof(MiniVariant), CallUpdate);
        }

        protected virtual void CallUpdate()
        {
        }

        public virtual Task HandleOnClickAsync(MouseEventArgs e)
        {
            return Task.CompletedTask;
        }

        public virtual async Task HandleOnMouseEnterAsync(MouseEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            await RunTaskInMicrosecondsAsync(() =>
                {
                    if (ExpandOnHover)
                    {
                        IsMouseover = true;
                    }
                },
                millisecondsDelay: 150,
                _cancellationTokenSource.Token);
        }

        public virtual async Task HandleOnMouseLeaveAsync(MouseEventArgs e)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();

            await RunTaskInMicrosecondsAsync(() =>
                {
                    if (ExpandOnHover)
                    {
                        IsMouseover = false;
                    }
                },
                millisecondsDelay: 150,
                _cancellationTokenSource.Token);
        }

        //TODO ontransitionend事件

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

        protected override void Dispose(bool disposing)
        {
            _disposed = true;
        }

        public async Task HandleOnOutsideClickAsync()
        {
            if (!CloseConditional()) return;
            IsActive = false;
        }
    }
}

using BlazorComponent.JSInterop;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class BDialog : BBootable, IAsyncDisposable
    {
        [Inject]
        public Document Document { get; set; }

        [Inject]
        public Window Window { get; set; }

        [Parameter]
        public string Attach { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public bool Fullscreen { get; set; }

        [Parameter]
        public bool HideOverlay { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public bool Persistent { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

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

        protected bool ShowOverlay => !Fullscreen && !HideOverlay;

        protected ElementReference? OverlayRef => ((BOverlay)Overlay)?.Ref;

        protected int StackMinZIndex { get; set; } = 200;

        protected virtual string AttachSelector { get; set; }

        public ElementReference ContentRef { get; set; }

        public ElementReference DialogRef { get; set; }

        protected object Overlay { get; set; }

        protected int ZIndex { get; set; }

        protected bool Attached { get; set; }

        protected bool Animated { get; set; }

        protected override async Task WhenIsActiveUpdating(bool value)
        {
            if (ContentRef.Context is not null)
            {
                await AttachAsync(value);
            }
            else
            {
                NextTick(() => AttachAsync(value));
            }

            if (value)
            {
                ZIndex = await GetActiveZIndex(true);

                await HideScroll();

                NextTick(async () =>
                {
                    // TODO: previousActiveElement

                    var contains = await JsInvokeAsync<bool>(JsInteropConstants.ContainsActiveElement, ContentRef);
                    if (!contains)
                    {
                        await JsInvokeAsync(JsInteropConstants.Focus, ContentRef);
                    }
                });
            }
            else
            {
                await ShowScroll();
            }

            await base.WhenIsActiveUpdating(value);
        }

        private async Task AttachAsync(bool value)
        {
            if (!Attached)
            {
                Attached = true;

                await Js.AddOutsideClickEventListener(HandleOnOutsideClickAsync,
                    new[] { Document.GetElementByReference(DialogRef).Selector },
                    new[] { Document.GetElementByReference(OverlayRef!.Value).Selector });

                await JsInvokeAsync(JsInteropConstants.AddElementTo, OverlayRef, AttachSelector);
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, AttachSelector);
            }

            StateHasChanged();
        }

        protected virtual bool IsFullscreen => Fullscreen;

        private async Task HideScroll()
        {
            await JsInvokeAsync(JsInteropConstants.HideScroll, IsFullscreen, OverlayRef.GetSelector(), ContentRef, DialogRef);
        }

        private async Task ShowScroll()
        {
            await JsInvokeAsync(JsInteropConstants.ShowScroll, OverlayRef.GetSelector());
        }

        protected async Task HandleOnOutsideClickAsync(object _)
        {
            if (!CloseConditional()) return;

            if (OnOutsideClick.HasDelegate)
            {
                await OnOutsideClick.InvokeAsync();
            }

            await CloseAsync();

            await InvokeStateHasChangedAsync();
        }

        private bool CloseConditional()
        {
            return IsActive;
        }

        private async Task<int> GetActiveZIndex(bool isActive)
        {
            return !isActive ? await JsInvokeAsync<int>(JsInteropConstants.GetZIndex, ContentRef) : await GetMaxZIndex() + 2;
        }

        private async Task<int> GetMaxZIndex()
        {
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> { ContentRef }, Ref);

            return maxZindex > StackMinZIndex ? maxZindex : StackMinZIndex;
        }

        public async Task Keydown(KeyboardEventArgs args)
        {
            if (args.Key == "Escape")
            {
                await CloseAsync();
            }
        }

        protected async Task CloseAsync()
        {
            if (Persistent)
            {
                Animated = true;
                StateHasChanged();
                NextTick(async () =>
                {
                    //This animated need 150ms
                    await Task.Delay(150);
                    Animated = false;
                    StateHasChanged();
                });
            }
            else
            {
                await SetIsActive(false);
            }
        }

        public new async ValueTask DisposeAsync()
        {
            await DeleteContent();
        }

        protected virtual async Task DeleteContent()
        {
            try
            {
                if (IsActive)
                {
                    await ShowScroll();
                }

                if (ContentRef.Context != null)
                {
                    await JsInvokeAsync(JsInteropConstants.DelElementFrom, ContentRef, AttachSelector);
                }

                if (OverlayRef?.Context != null)
                {
                    await JsInvokeAsync(JsInteropConstants.DelElementFrom, OverlayRef, AttachSelector);
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}

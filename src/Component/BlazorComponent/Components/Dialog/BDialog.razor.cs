using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public partial class BDialog : BBootable, IAsyncDisposable
    {
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

        [Inject]
        public Document Document { get; set; }

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

        protected override async Task OnActiveUpdated(bool value)
        {
            if (!Attached)
            {
                Attached = true;

                await JsInvokeAsync(JsInteropConstants.AddOutsideClickEventListener,
                    DotNetObjectReference.Create(new Invoker<object>(HandleOnOutsideClickAsync)),
                    new[] { Document.GetElementByReference(DialogRef).Selector },
                    new[] { Document.GetElementByReference(OverlayRef!.Value).Selector });

                ZIndex = await GetActiveZIndex(value);

                await JsInvokeAsync(JsInteropConstants.AddElementTo, OverlayRef, AttachSelector);
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, AttachSelector);
            }

            NextTick(async () =>
            {
                // TODO: previousActiveElement
                
                var contains = await JsInvokeAsync<bool>(JsInteropConstants.ContainsActiveElement, ContentRef);
                if (!contains)
                {
                    await JsInvokeAsync(JsInteropConstants.Focus, ContentRef);
                }
            });

            await base.OnActiveUpdated(value);
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
                await RunCloseDelayAsync();
            }
        }

        public async ValueTask DisposeAsync()
        {
            await DeleteContent();
        }

        protected virtual async Task DeleteContent()
        {
            try
            {
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
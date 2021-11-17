using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDialog : BActivatable, IAsyncDisposable
    {
        private int _stackMinZIndex = 200;
        private bool _isNotFirstRender = true;

        protected virtual string AttachSelector { get; set; }

        public ElementReference ContentRef { get; set; }

        public ElementReference DialogRef { get; set; }

        protected AbstractComponent Overlay { get; set; }

        protected int ZIndex { get; set; }

        [Parameter]
        public string Attach { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public StringNumber MaxWidth { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnOutsideClick { get; set; }

        [Parameter]
        public bool Persistent { get; set; }

        [Parameter]
        public StringNumber Width { get; set; }

        private bool _isHasOverlayElement;

        public override bool Value
        {
            get => base.Value;
            set
            {
                base.Value = value;
                if (value)
                {
                    _isHasOverlayElement = true;
                }
            }
        }

        protected override async Task OnParametersSetAsync()
        {
            if (!_isNotFirstRender)
            {
                if (ZIndex == default)
                {
                    ZIndex = await ActiveZIndex();
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                _isNotFirstRender = false;
            }

            await ShowLazyContent();
        }

        protected override async Task Close()
        {
            if (Persistent) return;

            await base.Close();
        }

        private async Task<int> ActiveZIndex()
        {
            int zIndex;
            if (!Value)
            {
                zIndex = await JsInvokeAsync<int>(JsInteropConstants.GetZIndex, ContentRef);
            }
            else
            {
                zIndex = await GetMaxZIndex() + 2;
            }

            return zIndex;
        }

        private async Task<int> GetMaxZIndex()
        {
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> {ContentRef}, Ref);

            return maxZindex > _stackMinZIndex ? maxZindex : _stackMinZIndex;
        }

        public virtual Task ShowLazyContent()
        {
            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (ContentRef.Context != null)
                await JsInvokeAsync(JsInteropConstants.DelElementFrom, ContentRef, AttachSelector);

            if (((BDomComponentBase)Overlay?.Instance).Ref.Context != null && _isHasOverlayElement)
                await JsInvokeAsync(JsInteropConstants.DelElementFrom, ((BDomComponentBase)Overlay.Instance).Ref, AttachSelector);

            GC.SuppressFinalize(this);
        }
    }
}
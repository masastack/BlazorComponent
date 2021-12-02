using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDialog : BActivatable, IAsyncDisposable
    {
        private bool _isHasOverlayElement;
        private bool _valueChangedToTrue;
        private int _stackMinZIndex = 200;

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

        public override bool Value
        {
            get => base.Value;
            set
            {
                _valueChangedToTrue = base.Value != value && value;

                base.Value = value;

                if (value)
                {
                    _isHasOverlayElement = true;
                }
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            await ShowLazyContent();
            
            if (_valueChangedToTrue)
            {
                ZIndex = await ActiveZIndex();
                await Show();
                StateHasChanged();
                _valueChangedToTrue = false;
            }
        }

        protected bool ShowOverlay => !Fullscreen && !HideOverlay;

        private async Task Show()
        {
            // TODO: previousActiveElement
            // https://github.com/vuetifyjs/vuetify/blob/master/packages/vuetify/src/components/VDialog/VDialog.ts#L185
            
            var contains = await JsInvokeAsync<bool?>(JsInteropConstants.ContainsActiveElement, ContentRef);
            if (contains.HasValue && !contains.Value)
            {
                await JsInvokeAsync(JsInteropConstants.Focus, ContentRef);
            }
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
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> { ContentRef }, Ref);

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
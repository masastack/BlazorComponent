using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BDialog : BActivatable
    {
        private int _stackMinZIndex = 200;
        private bool _isNotFirstRender = true;

        public ElementReference ContentRef { get; set; }

        public ElementReference DialogRef { get; set; }

        private AbstractComponent Overlay { get; set; }

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
                var overlayElement = ((BDomComponentBase)Overlay.Instance).Ref;
                await JsInvokeAsync(JsInteropConstants.AddElementTo, overlayElement, Attach ?? ".m-application");
                await JsInvokeAsync(JsInteropConstants.AddElementTo, ContentRef, Attach ?? ".m-application");
            }
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

        protected override async Task Close()
        {
            Value = false;
        }

        private async Task<int> GetMaxZIndex()
        {
            var maxZindex = await JsInvokeAsync<int>(JsInteropConstants.GetMenuOrDialogMaxZIndex, new List<ElementReference> {ContentRef}, Ref);

            return maxZindex > _stackMinZIndex ? maxZindex : _stackMinZIndex;
        }

        protected override async Task Open()
        {
            Value = true;
        }

        protected override async Task Toggle()
        {
            Value = !Value;
        }
    }
}
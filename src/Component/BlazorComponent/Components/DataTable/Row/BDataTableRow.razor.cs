using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableRow<TItem> : IAsyncDisposable
    {
        [Inject]
        private IResizeJSModule ResizeJSModule { get; set; } = null!;

        [Parameter]
        public List<DataTableHeader<TItem>> Headers { get; set; } = null!;

        [Parameter]
        public TItem Item { get; set; } = default!;

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public Func<ItemColProps<TItem>, bool> HasSlot { get; set; } = null!;

        [Parameter]
        public RenderFragment<ItemColProps<TItem>> SlotContent { get; set; } = null!;

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                foreach (var header in Headers)
                {
                    await ResizeJSModule.ObserverAsync(header.ElementReference, () =>OnResizeAsync(header));
                }
            }
        }

        private async Task OnResizeAsync(DataTableHeader header)
        {
            header.RealWidth = await Js.InvokeAsync<double>(JsInteropConstants.GetProp, header.ElementReference, "offsetWidth");
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            foreach (var header in Headers)
            {
                await ResizeJSModule.UnobserveAsync(header.ElementReference);
            }
        }
    }
}

using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BDataTableRow<TItem> : IAsyncDisposable
    {
        [Inject]
        private IResizeJSModule ResizeJSModule { get; set; } = null!;

        [CascadingParameter]
        private BSimpleTable SimpleTable { get; set; } = null!;

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

        private List<DataTableHeader<TItem>> NoSpecificWidthHeaders => Headers.Where(u => u.Width is null).ToList();

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            var lastFixedLeftHeader = Headers.LastOrDefault(u => u.Fixed == DataTableFixed.Left);
            if (lastFixedLeftHeader != null)
            {
                lastFixedLeftHeader.IsFixedShadowColumn = true;
            }

            var firstFixedRightHeader = Headers.FirstOrDefault(u => u.Fixed == DataTableFixed.Right);
            if (firstFixedRightHeader != null)
            {
                firstFixedRightHeader.IsFixedShadowColumn = true;
            }
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            await base.OnAfterRenderAsync(firstRender);

            if (firstRender)
            {
                foreach (var header in NoSpecificWidthHeaders)
                {
                    await ResizeJSModule.ObserverAsync(header.ElementReference, () => OnResizeAsync(header));
                }
            }
        }

        private async Task OnResizeAsync(DataTableHeader header)
        {
            header.RealWidth = await Js.InvokeAsync<double>(JsInteropConstants.GetProp, header.ElementReference, "offsetWidth");
            await SimpleTable.DebounceRenderForColResizeAsync();
        }

        async ValueTask IAsyncDisposable.DisposeAsync()
        {
            foreach (var header in NoSpecificWidthHeaders)
            {
                await ResizeJSModule.UnobserveAsync(header.ElementReference);
            }
        }
    }
}

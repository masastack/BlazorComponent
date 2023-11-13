using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSimpleTable
    {
        [Parameter]
        public RenderFragment? TopContent { get; set; }

        [Parameter]
        public RenderFragment? BottomContent { get; set; }

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

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

        private CancellationTokenSource _cancellationTokenSource = new();

        internal async Task DebounceRenderForColResizeAsync()
        {
            _cancellationTokenSource.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            await RunTaskInMicrosecondsAsync(StateHasChanged, 16 * 2, _cancellationTokenSource.Token);
        }
    }
}

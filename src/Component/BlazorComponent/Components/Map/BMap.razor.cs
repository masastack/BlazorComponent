using BlazorComponent.JSInterop;
using Microsoft.JSInterop;
using System.ComponentModel;

namespace BlazorComponent
{
    public partial class BMap : BDomComponentBase
    {
        [Parameter]
        [EditorRequired]
        public string? ServiceKey { get; set; }

        [Parameter]
        public StringNumber? Width { get; set; }

        [Parameter]
        public StringNumber? Height { get; set; }

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

        private bool jsHasInjected = false;
        private bool mapHasLoaded = false;
        private IJSObjectReference? mapModule = null;

        /// <summary>
        /// Inject BaiduMap Javascript code and load map. Click
        /// <see href="https://learn.microsoft.com/en-us/aspnet/core/blazor/javascript-interoperability/call-javascript-from-dotnet?view=aspnetcore-7.0">
        ///     here
        /// </see> for more details.
        /// </summary>
        /// <param name="firstRender"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            // 1st render, inject BaiduMap Javascript code
            if (firstRender && !jsHasInjected)
            {
                if (string.IsNullOrWhiteSpace(ServiceKey))
                    throw new ArgumentNullException
                        (
                            nameof(ServiceKey),
                            $"{nameof(ServiceKey)} is required to enable map services"
                        );

                mapModule = await JsRuntime.InvokeAsync<IJSObjectReference>("import", "./_content/BlazorComponent/js/map.js");
                await mapModule.InvokeVoidAsync("injectBaiduMapScript", ServiceKey);

                jsHasInjected = true;
                StateHasChanged();
            }

            // 2nd render, load map
            if (!firstRender && !mapHasLoaded)
            {
                if (mapModule is null)
                    return;

                await mapModule.InvokeVoidAsync("loadMap", Id);
                mapHasLoaded = true;
                StateHasChanged();
            }
        }

    }
}

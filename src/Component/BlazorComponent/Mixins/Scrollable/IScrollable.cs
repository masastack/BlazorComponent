using BlazorComponent.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public interface IScrollable
    {
        bool CanScroll { get; }

        string ScrollTarget { get; }

        double ScrollThreshold { get; }

        IJSRuntime Js { get; }
    }
}
using BlazorComponent.Web;
using Microsoft.JSInterop;

namespace BlazorComponent
{
    public interface IScrollable
    {
        public string ScrollTarget { get; }

        public double ScrollThreshold { get; }

        IJSRuntime Js { get; }

        HtmlElement Target { get; }
    }
}
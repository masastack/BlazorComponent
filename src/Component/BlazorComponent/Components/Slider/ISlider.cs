using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface ISlider<TValue> : IInput<TValue>, ILoadable
    {
        bool InverseLabel => default;

        Dictionary<string, object> InputAttrs { get; }

        ElementReference TrackElement { set; }

        double Step { get; }

        bool ShowTicks { get; }

        double TickSize { get; }

        double NumTicks { get; }

        bool Vertical { get; }

        List<string> TickLabels { get; }

        ElementReference ThumbElement { get; set; }

        Task HandleOnFocusAsync(FocusEventArgs args);

        Task HandleOnBlurAsync(FocusEventArgs args);

        Dictionary<string, object> ThumbAttrs { get; }

        bool ShowThumbLabel { get; }

        bool ShowThumbLabelContainer { get; }

        RenderFragment<int> ThumbLabelContent { get; }

        Task HandleOnSliderClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnSliderMouseDownAsync(ExMouseEventArgs args)
        {
            return Task.CompletedTask;
        }

        Task HandleOnKeyDownAsync(KeyboardEventArgs args);
    }
}

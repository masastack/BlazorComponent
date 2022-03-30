using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public interface ISelectable : IInput<bool>, IRippleable
    {
        bool IsDisabled { get; }

        Dictionary<string, object> InputAttrs { get; }

        bool IsActive { get; }

        Task HandleOnBlur(FocusEventArgs args);

        Task HandleOnChange(ChangeEventArgs args);

        Task HandleOnFocus(FocusEventArgs args);

        Task HandleOnKeyDown(KeyboardEventArgs args);
    }
}

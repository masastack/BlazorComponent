using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public interface IActivatableJsCallbacks : IDelayable, IOutsideClickJsCallback
{
    string ActivatorSelector { get; }

    bool Disabled { get; }

    bool OpenOnHover { get; }

    bool OpenOnClick { get; }

    bool OpenOnFocus { get; }

    Task SetActive(bool val);

    Task HandleOnClickAsync(MouseEventArgs args);
}

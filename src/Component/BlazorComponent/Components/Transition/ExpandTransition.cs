using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent;

/// <summary>
/// The ExpandTransition.
/// </summary>
public class ExpandTransition : Transition
{
    protected virtual string SizeProp => "height";

    private double? Size { get; set; }

    protected override void OnParametersSet()
    {
        Name = "expand-transition";
    }

    public override string GetClass(TransitionState transitionState)
    {
        var transitionClass = base.GetClass(transitionState);

        return string.Join(" ", transitionClass);
    }

    public override string GetStyle(TransitionState transitionState)
    {
        var styles = new List<string>
        {
            base.GetStyle(transitionState)
        };

        switch (transitionState)
        {
            case TransitionState.Enter:
            case TransitionState.LeaveTo:
                styles.Add("overflow:hidden");
                if (Size.HasValue)
                {
                    styles.Add($"{SizeProp}:0px");
                }

                break;
            case TransitionState.EnterTo:
            case TransitionState.Leave:
                styles.Add("overflow:hidden");
                if (Size.HasValue)
                {
                    styles.Add($"{SizeProp}:{Size}px");
                }

                break;
        }

        return string.Join(';', styles);
    }

    public override Task Enter(TransitionElementBase element)
    {
        Console.WriteLine($"{element.Reference.Id} enter");
        return UpdateSize(element.Reference);
    }

    public override Task Leave(TransitionElementBase element)
    {
        Console.WriteLine($"{element.Reference.Id} leave");
        return UpdateSize(element.Reference);
    }

    // public override Task AfterEnter(TransitionElementBase element)
    // {
    //     Console.WriteLine($"{element.Reference.Id} after enter");
    //     Size = null;
    //     return Task.CompletedTask;
    // }
    //
    // public override Task AfterLeave(TransitionElementBase element)
    // {
    //     Console.WriteLine($"{element.Reference.Id} after leave");
    //     Size = null;
    //     return Task.CompletedTask;
    // }

    // public override Task EnterCancelled(TransitionElementBase element)
    // {
    //     Console.WriteLine($"{element.Reference.Id} enter cancel");
    //     Size = null;
    //
    //     if (TransitionElement is not null)
    //     {
    //         TransitionElement.CurrentState = TransitionState.None;
    //         StateHasChanged();
    //     }
    //
    //     return Task.CompletedTask;
    // }
    //
    // public override Task LeaveCancelled(TransitionElementBase element)
    // {
    //     Console.WriteLine($"{element.Reference.Id} leave cancel");
    //     Size = null;
    //
    //     if (TransitionElement is not null)
    //     {
    //         TransitionElement.CurrentState = TransitionState.None;
    //         StateHasChanged();
    //     }
    //
    //     return Task.CompletedTask;
    // }

    private async Task UpdateSize(ElementReference elementReference)
    {
        var elementInfo = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, elementReference);
        var size = elementInfo.OffsetHeight;
        if (size != 0)
        {
            Size = size;
        }

        Console.WriteLine($"Size:{Size}");
    }
}

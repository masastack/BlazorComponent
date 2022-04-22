using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent;

/// <summary>
/// The ExpandTransition.
/// </summary>
public class ExpandTransition : Transition
{
    // BUG: Unable to get height/width for the first time.
    // TODO: Try to rewrite ExpandTransition with hooks.
    // https://github.com/vuetifyjs/vuetify/blob/aa68dd2d9c/packages/vuetify/src/components/transitions/expand-transition.ts

    [Inject]
    public IJSRuntime Js { get; set; }

    protected virtual string SizeProp => "height";

    private double Size { get; set; }

    protected override void OnParametersSet()
    {
        Name = "expand-transition";
    }

    public override string GetClass(TransitionState transitionState)
    {
        var transitionClass = base.GetClass(transitionState);

        return string.Join(
            " ",
            transitionClass,
            transitionState == TransitionState.None ? null : "in-transition");
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
                styles.Add($"{SizeProp}:0px");
                break;
            case TransitionState.EnterTo:
            case TransitionState.Leave:
                styles.Add($"{SizeProp}:{Size}px");
                break;
        }

        if (transitionState != TransitionState.None)
        {
            styles.Add("overflow:hidden");
        }

        return string.Join(';', styles);
    }

    public override async Task OnElementReadyAsync(ElementReference elementReference)
    {
        await Js.InvokeVoidAsync(JsInteropConstants.ObserveElement, elementReference, SizeProp, DotNetObjectReference.Create(this));
    }

    [JSInvokable]
    public void OnSizeChanged(double size, string blazorId)
    {
        Console.WriteLine($"blazorId:{blazorId}");
        Console.WriteLine($"ElementReference?.Id:{ElementReference?.Id}");
        Console.WriteLine($"size:{size}");
        if (ElementReference?.Id == blazorId)
        {
            Size = size;
        }
    }
}
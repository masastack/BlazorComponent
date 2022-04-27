using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace BlazorComponent;

/// <summary>
/// The ExpandTransition.
/// </summary>
public class ExpandTransition : Transition
{
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
                styles.Add("overflow:hidden");
                styles.Add($"{SizeProp}:0px");
                break;
            case TransitionState.EnterTo:
            case TransitionState.Leave:
                styles.Add($"{SizeProp}:{Size}px");
                styles.Add("overflow:hidden");
                break;
        }

        return string.Join(';', styles);
    }

    public override Task Enter(TransitionElementBase element)
    {
        return UpdateSize(element.Reference);
    }

    public override Task Leave(TransitionElementBase element)
    {
        return UpdateSize(element.Reference);
    }

    private async Task UpdateSize(ElementReference elementReference)
    {
        var elementInfo = await Js.InvokeAsync<BlazorComponent.Web.Element>(JsInteropConstants.GetDomInfo, elementReference);
        Size = elementInfo.OffsetHeight;
    }
}
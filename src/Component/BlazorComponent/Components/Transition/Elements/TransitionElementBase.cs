using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent;

public abstract class TransitionElementBase : Element
{
    private ElementReference? _reference;

    protected bool ElementReferenceChanged { get; set; }

    /// <summary>
    /// The dom information about the transitional element.
    /// </summary>
    internal BlazorComponent.Web.Element? ElementInfo { get; set; }

    public ElementReference Reference
    {
        get => _reference ?? new ElementReference();
        protected set
        {
            if (_reference.HasValue && _reference.Value.Id != value.Id)
            {
                ElementReferenceChanged = true;
            }

            _reference = value;
        }
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        builder.OpenElement(0, (Tag ?? "div"));
        builder.AddMultipleAttributes(1, AdditionalAttributes);
        builder.AddAttribute(2, "class", ComputedClass);
        builder.AddAttribute(3, "style", ComputedStyle);
        builder.AddContent(4, ChildContent);
        builder.AddElementReferenceCapture(5, reference =>
        {
            Reference = reference;
            ReferenceCaptureAction?.Invoke(reference);
        });
        builder.CloseElement();
    }
}

#if NET6_0
public abstract class TransitionElementBase<TValue> : TransitionElementBase
#else
public abstract class TransitionElementBase<TValue> : TransitionElementBase where TValue : notnull
#endif
{
    [CascadingParameter] public Transition? Transition { get; set; }

    [Parameter, EditorRequired] public TValue Value { get; set; } = default!;
}
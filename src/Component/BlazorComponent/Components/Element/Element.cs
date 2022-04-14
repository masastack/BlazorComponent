using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent
{
    public class Element : ComponentBase
    {
        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public Action<ElementReference> ReferenceCaptureAction { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public virtual IDictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

        private ElementReference _reference;
        protected bool _referenceChanged;

        protected string Id => AdditionalAttributes.TryGetValue("id", out var id) ? id.ToString() : string.Empty;

        protected virtual string ComputedClass => Class;

        protected virtual string ComputedStyle => Style;

        public ElementReference Reference
        {
            get => _reference;
            protected set
            {
                if (_reference.Id != value.Id)
                {
                    _referenceChanged = true;
                }

                _reference = value;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;
            builder.OpenElement(sequence++, Tag);

            builder.AddMultipleAttributes(sequence++, AdditionalAttributes);
            builder.AddAttribute(sequence++, "class", ComputedClass);
            builder.AddAttribute(sequence++, "style", ComputedStyle);
            builder.AddContent(sequence++, ChildContent);
            builder.AddElementReferenceCapture(sequence++, reference =>
            {
                ReferenceCaptureAction?.Invoke(reference);
                Reference = reference;
            });

            builder.CloseComponent();
        }
    }
}
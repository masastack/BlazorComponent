using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Components;

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
        public virtual IDictionary<string, object> AdditionalAttributes { get; set; }

        protected virtual string ComputedClass => Class;

        protected virtual string ComputedStyle => Style;

        public ElementReference Reference { get; protected set; }

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

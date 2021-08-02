using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class Element : ComponentBase
    {
        [Parameter]
        public string Tag { get; set; } = "div";

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> ExtraAttributes { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Action<ElementReference> ReferenceCaptureAction { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;
            builder.OpenElement(sequence++, Tag);

            foreach (var attr in ExtraAttributes)
            {
                builder.AddMultipleAttributes(sequence++, ExtraAttributes);
            }
            builder.AddContent(sequence++, ChildContent);

            builder.AddElementReferenceCapture(sequence++, ReferenceCaptureAction);

            builder.CloseElement();
        }
    }
}

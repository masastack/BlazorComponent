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
        public IDictionary<string, object> ExtraAttributes { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Action<ElementReference> ReferenceCaptureAction { get; set; }

        [Parameter]
        public string Class { get; set; }

        [Parameter]
        public string Style { get; set; }

        [Parameter]
        public bool Show
        {
            get
            {
                return Transition?.Value ?? false;
            }
            set
            {
                if (Transition != null)
                {
                    Transition.Value = value;
                }
            }
        }

        [CascadingParameter]
        public Transition Transition { get; set; }

        protected string ComputedClass
        {
            get
            {
                var @class = Class;

                if (Transition != null)
                {
                    @class = string.Join(' ', Class, Transition.Class);
                }

                if (string.IsNullOrWhiteSpace(@class))
                {
                    return null;
                }

                return @class;
            }
        }

        protected string ComputedStyle
        {
            get
            {
                var style = Style;

                if (Transition != null)
                {
                    style = string.Join(';', Style, Transition.Style).Trim();
                }

                if (string.IsNullOrWhiteSpace(style))
                {
                    return null;
                }

                return style;
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;
            builder.OpenElement(sequence++, Tag);

            builder.AddAttribute(sequence++, "class", ComputedClass);
            builder.AddAttribute(sequence++, "style", ComputedStyle);

            if (Transition != null)
            {
                builder.AddAttribute(sequence++, Transition.Id, true);
            }

            builder.AddMultipleAttributes(sequence++, ExtraAttributes);

            if (ChildContent != null)
            {
                if (Transition != null)
                {
                    var childContent = ChildContent;
                    ChildContent = transitionBuilder =>
                    {
                        transitionBuilder.OpenComponent<CascadingValue<Transition>>(sequence++);
                        transitionBuilder.AddAttribute(sequence++, "Value", (Transition)null);
                        transitionBuilder.AddAttribute(sequence++, "ChildContent", childContent);
                        transitionBuilder.AddAttribute(sequence++, "IsFixed", true);
                        transitionBuilder.CloseComponent();
                    };
                }

                builder.AddContent(sequence++, ChildContent);
            }

            if (ReferenceCaptureAction != null)
            {
                builder.AddElementReferenceCapture(sequence++, ReferenceCaptureAction);
            }

            builder.CloseElement();
        }
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.RenderTree;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorComponent
{
    public class AbstractComponent : ComponentBase
    {
        [Parameter]
        public AbstractMetadata Metadata { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter(CaptureUnmatchedValues = true)]
        public Dictionary<string, object> AdditionalAttributes { get; set; } = new Dictionary<string, object>();

        public object? Instance { get; private set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var type = Metadata.Type;
            var props = Metadata.Properties;

            var sequence = 0;
            builder.OpenComponent(sequence++, type);

            if (props != null)
            {
                builder.AddMultipleAttributes(sequence++, props);
            }

            builder.AddMultipleAttributes(sequence++, AdditionalAttributes);

            if (ChildContent != null)
            {
                builder.AddAttribute(sequence++, nameof(ChildContent), ChildContent);
            }

            builder.AddComponentReferenceCapture(sequence++, component => Instance = component);

            builder.CloseComponent();
        }
    }
}

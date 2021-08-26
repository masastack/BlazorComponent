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

#pragma warning disable BL0006
        protected override void OnParametersSet()
        {
            if (Metadata == null)
            {
                throw new ArgumentNullException(nameof(Metadata));
            }

            if (ChildContent != null)
            {
                var builder = new RenderTreeBuilder();
                ChildContent(builder);

                var frames = builder.GetFrames().Array;
                foreach (var frame in frames)
                {
                    if (frame.FrameType == RenderTreeFrameType.Component && frame.ComponentType.IsAssignableTo(typeof(IAbstractContent)))
                    {
                        var nameFrame = frames.First(u => u.Sequence == frame.Sequence + 1);
                        var contentFrame = frames.First(u => u.Sequence == frame.Sequence + 2);

                        if (nameFrame.AttributeValue.ToString() == nameof(AbstractContent.ChildContent))
                        {
                            //Here may cause a bug
                            ChildContent = (RenderFragment)contentFrame.AttributeValue;
                        }
                        else
                        {
                            if (!AdditionalAttributes.ContainsKey(nameFrame.AttributeValue.ToString()))
                            {
                                AdditionalAttributes.Add(nameFrame.AttributeValue.ToString(), contentFrame.AttributeValue);
                            }
                        }
                    }
                }
            }
        }
#pragma warning restore BL0006

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var type = Metadata.Type;
            var props = Metadata.Properties;

            var sequence = 0;
            builder.OpenComponent(sequence++, type);

            //Set props
            if (props != null)
            {
                foreach (var prop in props)
                {
                    builder.AddAttribute(sequence++, prop.Key, prop.Value);
                }
            }

            //Set additional attributes
            AdditionalAttributes.ForEach(attr => builder.AddAttribute(sequence++, attr.Key, attr.Value));

            //Set child content 
            if (ChildContent != null)
            {
                builder.AddAttribute(sequence++, nameof(ChildContent), ChildContent);
            }

            builder.CloseComponent();
        }
    }
}

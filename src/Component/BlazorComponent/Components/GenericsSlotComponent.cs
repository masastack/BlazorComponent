using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent
{
    public class GenericsSlotComponent<T> : ComponentBase
    {
        [Parameter]
        public SlotComponentDescription Description { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public T Value { get; set; }

        [Parameter]
        public string Label { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Description == null)
            {
                throw new ArgumentNullException(nameof(Description));
            }

            if (!Description.Type.IsAssignableTo(typeof(IComponent)))
            {
                throw new InvalidOperationException("Type should be a component type.");
            }

            if (Description.Type.IsAbstract || Description.Type.IsInterface || !Description.Type.IsClass)
            {
                throw new InvalidOperationException("Type should be not abstract class.");
            }

            var sequence = 0;
            builder.OpenComponent(sequence++, Description.Type);

            if (Description.Properties != null)
            {
                foreach (var property in Description.Properties)
                {
                    builder.AddAttribute(sequence++, property.Key, property.Value);
                }
            }

            builder.AddAttribute(sequence++, Label, Value);

            if (!Description.Properties.ContainsKey(nameof(ChildContent)))
            {
                builder.AddAttribute(sequence++, nameof(ChildContent), ChildContent);
            }

            builder.CloseComponent();
        }
    }
}

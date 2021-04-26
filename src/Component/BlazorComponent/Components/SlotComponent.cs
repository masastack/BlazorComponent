using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent
{
    public class SlotComponent : ComponentBase
    {

        [Parameter]
        public IDictionary<string, object> Properties { get; set; }

        [Parameter]
        public Type Type { get; set; }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            if (Type == null)
            {
                throw new ArgumentNullException(nameof(Type));
            }

            if (!Type.IsAssignableTo(typeof(IComponent)))
            {
                throw new InvalidOperationException("Type should be a component type.");
            }

            if (Type.IsAbstract || Type.IsInterface || !Type.IsClass)
            {
                throw new InvalidOperationException("Type should be not abstract class.");
            }

            var sequence = 0;
            builder.OpenComponent(sequence++, Type);

            if (Properties != null)
            {
                foreach (var property in Properties)
                {
                    builder.AddAttribute(sequence++, property.Key, property.Value);
                }
            }

            builder.CloseComponent();
        }
    }
}

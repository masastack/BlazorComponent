using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorComponent
{
    public class BCascadingValue<TValue> : ComponentBase
    {
        private Type _cascadingValueType;

        [Parameter]
        public TValue Value { get; set; }

        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public bool IsFixed { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnInitialized()
        {
            if (_cascadingValueType == null)
            {
                _cascadingValueType = typeof(CascadingValue<>).MakeGenericType(Value.GetType());
            }
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;
            builder.OpenComponent(sequence++, _cascadingValueType);
            builder.AddAttribute(sequence++, nameof(Value), Value);
            if (!string.IsNullOrEmpty(Name))
            {
                builder.AddAttribute(sequence++, nameof(Name), Name);
            }
            builder.AddAttribute(sequence++, nameof(IsFixed), IsFixed);
            builder.AddAttribute(sequence++, nameof(ChildContent), ChildContent);
            builder.CloseComponent();
        }
    }

}
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTextFieldAffix<TValue, TInput> where TInput : ITextField<TValue>
    {
        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        [Parameter]
        public Action<ElementReference> ReferenceCapture { get; set; }

        public virtual string ComputedType => $"text-field-{Type}";

        public ElementReference Element
        {
            set
            {
                ReferenceCapture?.Invoke(value);
            }
        }
    }
}

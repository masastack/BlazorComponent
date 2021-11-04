using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class GroupElement : Element
    {
        [Parameter]
        public bool Vertical { get; set; }

        [Inject]
        public IJSRuntime JS { get; set; }

        public async Task OnTransition()
        {
            if (Vertical)
            {
                await JS.InvokeVoidAsync(JsInteropConstants.InsertToFirst, Reference);
            }
        }

        public override Task UpdateViewAsync()
        {
            return Task.CompletedTask;
        }

        protected override void BuildRenderTree(RenderTreeBuilder builder)
        {
            var sequence = 0;

            builder.OpenComponent<Transition>(sequence++);
            builder.AddAttribute(sequence++, nameof(ChildContent), CreateElement(true));
            builder.CloseComponent();

            builder.OpenComponent<Transition>(sequence++);
            builder.AddAttribute(sequence++, nameof(Transition.OnAfterEnter), (Func<Task>)OnTransition);
            builder.AddAttribute(sequence++, nameof(ChildContent), CreateElement(false));
            builder.CloseComponent();
        }

        private RenderFragment CreateElement(bool @if)
        {
            return builer =>
            {
                var sequence = 0;
                builer.OpenComponent<Element>(sequence++);
                builer.AddAttribute(sequence++, nameof(Tag), Tag);
                builer.AddAttribute(sequence++, nameof(Key), Key);
                builer.AddAttribute(sequence++, nameof(If), @if);
                builer.AddAttribute(sequence++, nameof(ReferenceCaptureAction), (Action<ElementReference>)(el => Reference = el));
                builer.AddAttribute(sequence++, nameof(ChildContent), ChildContent);
                builer.CloseComponent();
            };
        }
    }
}

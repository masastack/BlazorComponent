using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTextFieldAppendSlot<TValue>
    {
        public string AppendOuterIcon => Component.AppendOuterIcon;

        public RenderFragment AppendOuterContent => Component.AppendOuterContent;

        public EventCallback<MouseEventArgs> HandleOnAppendOuterClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnAppendOuterClickAsync);
    }
}

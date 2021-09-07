using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldAppendSlot<TValue>
    {
        public string AppendOuterIcon => Component.AppendOuterIcon;

        public RenderFragment AppendOuterContent => Component.AppendOuterContent;

        public EventCallback<MouseEventArgs> HandleOnAppendOuterClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnAppendOuterClickAsync);
    }
}

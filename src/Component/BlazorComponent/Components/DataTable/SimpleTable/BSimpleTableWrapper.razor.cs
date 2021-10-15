using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSimpleTableWrapper<TComponent> where TComponent : ISimpleTable
    {
        public RenderFragment WrapperContent => Component.WrapperContent;

        public RenderFragment ComponentChildContent => Component.ChildContent;

        public EventCallback<EventArgs> HandleOnScrollAsync => CreateEventCallback<EventArgs>(Component.HandleOnScrollAsync);

        public ElementReference WrapperElement
        {
            set
            {
                Component.WrapperElement = value;
            }
        }
    }
}

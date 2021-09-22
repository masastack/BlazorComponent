using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSliderThumbContainer<TInput> where TInput : ISlider
    {
        public ElementReference ThumbElement
        {
            set
            {
                Component.ThumbElement = value;
            }
        }

        public Dictionary<string, object> ThumbAttrs => Component.ThumbAttrs;

        public EventCallback<FocusEventArgs> HandleOnFocusAsync => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnFocusAsync);

        public EventCallback<FocusEventArgs> HandleOnBlurAsync => EventCallback.Factory.Create<FocusEventArgs>(Component, Component.HandleOnBlurAsync);

        public EventCallback<KeyboardEventArgs> HandleOnKeyDownAsync => EventCallback.Factory.Create<KeyboardEventArgs>(Component, Component.HandleOnKeyDownAsync);

        public bool ShowThumbLabel => Component.ShowThumbLabel;
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BRangeSliderThumbContainer<TValue, TRangeSlider> where TRangeSlider : IRangeSlider<TValue>
    {
        public ElementReference ThumbElement
        {
            set
            {
                Component.ThumbElement = value;
            }
        }

        public ElementReference SecondThumbElement
        {
            set
            {
                Component.SecondThumbElement = value;
            }
        }

        public Dictionary<string, object> ThumbAttrs => Component.ThumbAttrs;

        public EventCallback<FocusEventArgs> OnFocus => CreateEventCallback<FocusEventArgs>(Component.HandleOnFocusAsync);

        public EventCallback<FocusEventArgs> OnBlur => CreateEventCallback<FocusEventArgs>(Component.HandleOnBlurAsync);

        public EventCallback<FocusEventArgs> OnSecondFocus => CreateEventCallback<FocusEventArgs>(Component.HandleOnSecondFocusAsync);

        public EventCallback<FocusEventArgs> OnSecondBlur => CreateEventCallback<FocusEventArgs>(Component.HandleOnSecondBlurAsync);

        public EventCallback<KeyboardEventArgs> OnKeyDown => CreateEventCallback<KeyboardEventArgs>(Component.HandleOnKeyDownAsync);

        public bool ShowThumbLabel => Component.ShowThumbLabel;
    }
}

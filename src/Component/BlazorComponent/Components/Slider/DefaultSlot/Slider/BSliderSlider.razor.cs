using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSliderSlider<TInput> where TInput : ISlider
    {
        public EventCallback<MouseEventArgs> HandleOnSliderClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnSliderClickAsync);

        public EventCallback<ExMouseEventArgs> HandleOnSliderMouseDownAsync => EventCallback.Factory.Create<ExMouseEventArgs>(Component, Component.HandleOnSliderMouseDownAsync);
    }
}

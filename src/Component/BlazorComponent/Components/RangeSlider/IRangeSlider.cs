using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IRangeSlider<TValue> : ISlider<IList<TValue>>
    {
        ElementReference SecondThumbElement { set; }

        Task HandleOnSecondFocusAsync(FocusEventArgs args);

        Task HandleOnSecondBlurAsync(FocusEventArgs args);
    }
}


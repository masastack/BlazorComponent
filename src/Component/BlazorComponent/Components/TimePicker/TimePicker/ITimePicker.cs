using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ITimePicker : IHasProviderComponent
    {
        SelectingTimes Selecting { get; }

        bool AmPmInTitle { get; }

        bool IsAmPm { get; }

        Task HandleOnAmClickAsync(MouseEventArgs args);

        Task HandleOnPmClickAsync(MouseEventArgs args);
    }
}

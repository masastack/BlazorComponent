using System;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    [EventHandler("onmouseleave", typeof(MouseEventArgs), true, true)]
    [EventHandler("onmouseenter", typeof(MouseEventArgs), true, true)]
    public static class EventHandlers
    {
    }
}

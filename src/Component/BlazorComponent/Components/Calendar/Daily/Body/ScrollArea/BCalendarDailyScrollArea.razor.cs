using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyScrollArea<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        public ElementReference ScrollAreaElement
        { 
            set 
            {
                Component.ScrollAreaElement = value;
            } 
        }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCalendarDailyPane<TCalendarDaily> where TCalendarDaily : ICalendarDaily
    {
        public ElementReference PaneElement
        {
            set
            {
                Component.PaneElement = value;
            }
        }
    }
}

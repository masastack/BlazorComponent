using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTitleYearBtn<TDatePickerTitle> where TDatePickerTitle : IDatePickerTitle
    {
        public string Year => Component.Year;

        public string YearIcon => Component.YearIcon;

        public EventCallback<MouseEventArgs> OnYearBtnClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnYearBtnClickAsync);
    }
}

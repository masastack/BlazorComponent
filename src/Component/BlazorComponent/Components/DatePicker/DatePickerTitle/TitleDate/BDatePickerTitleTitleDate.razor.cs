using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTitleTitleDate<TDatePickerTitle> where TDatePickerTitle : IDatePickerTitle
    {
        public EventCallback<MouseEventArgs> OnTitleDateBtnClick => CreateEventCallback<MouseEventArgs>(Component.HandleOnTitleDateBtnClickAsync);
    }
}

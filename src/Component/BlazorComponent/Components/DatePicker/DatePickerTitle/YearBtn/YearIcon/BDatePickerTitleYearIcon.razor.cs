using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTitleYearIcon<TDatePickerTitle> where TDatePickerTitle : IDatePickerTitle
    {
        public string YearIcon => Component.YearIcon;
    }
}

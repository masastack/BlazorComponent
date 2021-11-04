using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerMonthTableTBody<TDatePickerMonthTable> where TDatePickerMonthTable : IDatePickerMonthTable
    {
        public int DisplayedYear => Component.DisplayedYear;
    }
}

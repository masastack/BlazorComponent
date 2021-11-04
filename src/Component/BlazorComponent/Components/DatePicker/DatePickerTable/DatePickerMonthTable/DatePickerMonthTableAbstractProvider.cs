using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class DatePickerMonthTableAbstractProvider
    {
        public static ComponentAbstractProvider ApplyDatePickerMonthTableDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BDatePickerTableSlot<>), typeof(BDatePickerMonthTableSlot<IDatePickerMonthTable>))
                .Apply(typeof(BDatePickerMonthTableTBody<>), typeof(BDatePickerMonthTableTBody<IDatePickerMonthTable>));
        }
    }
}

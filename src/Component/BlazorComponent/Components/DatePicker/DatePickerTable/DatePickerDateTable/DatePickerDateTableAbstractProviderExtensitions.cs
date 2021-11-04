using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class DatePickerDateTableAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyDatePickerDateTableDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BDatePickerTableSlot<>), typeof(BDatePickerDateTableSlot<IDatePickerDateTable>))
                .Apply(typeof(BDatePickerDateTableTHead<>), typeof(BDatePickerDateTableTHead<IDatePickerDateTable>))
                .Apply(typeof(BDatePickerDateTableTR<>), typeof(BDatePickerDateTableTR<IDatePickerDateTable>))
                .Apply(typeof(BDatePickerDateTableTBody<>), typeof(BDatePickerDateTableTBody<IDatePickerDateTable>))
                .Apply(typeof(BDatePickerDateTableWeekNumber<>), typeof(BDatePickerDateTableWeekNumber<IDatePickerDateTable>));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class DatePickerTableAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyDatePickerTableDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BDatePickerTableSlot<>), typeof(BDatePickerTableSlot<IDatePickerTable>))
                .Apply(typeof(BDatePickerTableButton<>), typeof(BDatePickerTableButton<IDatePickerTable>))
                .Apply(typeof(BDatePickerTableEvents<>), typeof(BDatePickerTableEvents<IDatePickerTable>));
        }
    }
}

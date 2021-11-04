using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class DatePickerYearsAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplyDatePickerYearsDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                 .Apply(typeof(BDatePickerYearsYearItems<>), typeof(BDatePickerYearsYearItems<IDatePickerYears>))
                 .Apply(typeof(BDatePickerYearsYearItem<>), typeof(BDatePickerYearsYearItem<IDatePickerYears>));
        }
    }
}

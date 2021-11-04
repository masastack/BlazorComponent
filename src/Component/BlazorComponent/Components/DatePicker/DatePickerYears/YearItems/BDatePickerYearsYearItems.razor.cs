using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerYearsYearItems<TDatePickerYears> where TDatePickerYears : IDatePickerYears
    {
        public int Value=>Component.Value;

        public DateOnly? Max =>Component.Max;

        public DateOnly? Min =>Component.Min;
    }
}

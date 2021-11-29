using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IDatePickerYears : IHasProviderComponent
    {
        int Value { get; }

        DateOnly? Max { get; }

        DateOnly? Min { get; }

        Func<DateOnly, string> Formatter { get; }

        Task HandleOnYearItemClickAsync(int year);
    }
}

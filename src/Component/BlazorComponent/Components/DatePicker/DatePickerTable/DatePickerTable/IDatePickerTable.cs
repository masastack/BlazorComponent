using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IDatePickerTable : IHasProviderComponent
    {
        int DisplayedYear { get; }

        Dictionary<string, object> GetButtonAttrs(DateOnly value);

        Func<DateOnly, string> Formatter { get; }
    }
}

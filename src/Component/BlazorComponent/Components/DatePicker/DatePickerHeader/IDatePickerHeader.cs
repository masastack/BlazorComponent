using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IDatePickerHeader : IHasProviderComponent
    {
        bool RTL { get; }

        string PrevIcon { get; }

        string NextIcon { get; }

        string Transition { get; }

        DateOnly Value { get; }

        Dictionary<string, object> ButtonAttrs { get; }

        RenderFragment ChildContent { get; }

        Func<DateOnly, string> Formatter { get; }
    }
}

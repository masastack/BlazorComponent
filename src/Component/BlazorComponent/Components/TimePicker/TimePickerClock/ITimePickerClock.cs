using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ITimePickerClock : IHasProviderComponent
    {
        int Min { get; }

        int Max { get; }

        int Step { get; }

        Func<int, string> Format { get; }
    }
}

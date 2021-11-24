using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public interface ICascader<TItem, TValue> : ISelect<TItem, TValue, TValue>
    {
        Func<TItem, List<TItem>> ItemChildren { get; }

        Func<TItem, Task> LoadChildren { get; }
    }
}

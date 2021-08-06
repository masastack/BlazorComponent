using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISelect<TItem, TValue> : ITextField<TValue>
    {
        bool Multiple { get; set; }

        List<TValue> Values { get; set; }

        Task SetSelectedAsync(string label, TValue value);

        Task RemoveSelectedAsync(string label, TValue value);

        void SetVisible(bool v);

        bool Chips { get; }

        List<string> Text { get; }

        IReadOnlyList<TItem> Items { get; }

        Func<TItem, TValue> ItemValue { get; }

        Func<TItem, bool> ItemDisabled { get; }

        Func<TItem, string> ItemText { get; }

        void SetOnExtraClick(Func<MouseEventArgs, Task> onExtraClick);
    }
}

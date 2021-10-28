using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISelect<TItem, TItemValue, TValue> : ITextField<TValue>
    {
        bool Multiple { get; }

        IList<TItemValue> Values { get; }
        Task SetSelectedAsync(string label, TItemValue value);

        Task RemoveSelectedAsync(string label, TItemValue value);

        void SetVisible(bool v);

        bool Chips { get; }

        List<string> Text { get; }

        IReadOnlyList<TItem> Items { get; }

        Func<TItem, TItemValue> ItemValue { get; }

        Func<TItem, bool> ItemDisabled { get; }

        Func<TItem, string> ItemText { get; }

        IReadOnlyList<TItem> ComputedItems { get; }

        int HighlightIndex { get; }

        string QueryText { get; }

        RenderFragment PrependItemContent => default;

        RenderFragment AppendItemContent => default;

        RenderFragment<int> SelectionContent => default;

        IList<TItem> SelectedItems { get; }
        
        AbstractComponent Menu { set; }
    }
}

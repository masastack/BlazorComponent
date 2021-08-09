using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectMenu<TItem, TValue, TInput> where TInput : ISelect<TItem, TValue>
    {
        public Action<Func<MouseEventArgs, Task>> SetOnExtraClick => Component.SetOnExtraClick;

        public IReadOnlyList<TItem> ComputedItems => Component.ComputedItems;

        public Func<TItem, string> ItemText => Component.ItemText;

        public Func<TItem, TValue> ItemValue => Component.ItemValue;

        public Func<TItem, bool> ItemDisabled => Component.ItemDisabled;

        public RenderFragment InputChildContent => Component.ChildContent;

        public int HighlightIndex => Component.HighlightIndex;
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectMenu<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        public IReadOnlyList<TItem> ComputedItems => Component.ComputedItems;

        public Func<TItem, string> ItemText => Component.ItemText;

        public Func<TItem, TItemValue> ItemValue => Component.ItemValue;

        public Func<TItem, bool> ItemDisabled => Component.ItemDisabled;

        public RenderFragment InputChildContent => Component.ChildContent;

        public int HighlightIndex => Component.HighlightIndex;

        public RenderFragment PrependItemContent => Component.PrependItemContent;

        public RenderFragment AppendItemContent => Component.AppendItemContent;

        public object Menu
        {
            set
            {
                Component.Menu = value;
            }
        }

        public bool HideSelected => Component.HideSelected;

        public IList<TItemValue> Values => Component.Values;

        public bool HideNoData => Component.HideNoData;

        public RenderFragment NoDataContent => Component.NoDataContent;
    }
}

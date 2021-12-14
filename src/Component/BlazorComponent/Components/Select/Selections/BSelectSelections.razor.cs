using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BSelectSelections<TItem, TItemValue, TValue, TInput> where TInput : ISelect<TItem, TItemValue, TValue>
    {
        public List<string> Text => Component.Text;

        public bool Chips => Component.Chips;

        public bool Multiple => Component.Multiple;

        public RenderFragment<SelectSelectionProps<TItem>> SelectionContent => Component.SelectionContent;

        public IList<TItem> SelectedItems => Component.SelectedItems;
    }
}

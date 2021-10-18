using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataIteratorItems<TItem, TComponent> where TComponent : IDataIterator<TItem>
    {
        public bool IsEmpty => Component.IsEmpty;

        public RenderFragment ComponentChildContent => Component.ChildContent;

        public RenderFragment<(int Index, TItem Item)> ItemContent => Component.ItemContent;

        public IEnumerable<TItem> ComputedItems =>Component.ComputedItems;
    }
}

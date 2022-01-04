using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectListTile<TItem, TItemValue, TValue>
    {
        [Parameter]
        public TItem Item { get; set; }

        [Parameter]
        public int Index { get; set; }

        [Parameter]
        public bool? Disabled { get; set; }

        [Parameter]
        public bool Value { get; set; }

        protected Func<TItem, bool> HasItem => Component.HasItem;

        protected bool Action => Component.Action;

        protected bool HideSelected => Component.HideSelected;

        protected IList<TItem> Items => Component.Items;

        protected RenderFragment<SelectListItemProps<TItem>> ItemContent => Component.ItemContent;
    }
}

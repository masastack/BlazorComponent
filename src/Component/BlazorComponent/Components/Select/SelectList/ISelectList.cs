using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISelectList<TItem, TItemValue, TValue> : IHasProviderComponent
    {
        bool Action { get; }
        bool HideSelected { get; }
        IList<TItem> Items { get; }
        string NoDataText { get; }
        RenderFragment<SelectListItemProps<TItem>> ItemContent { get; }
        string GetFilteredText(TItem item);
        bool HasItem(TItem item);
    }
}

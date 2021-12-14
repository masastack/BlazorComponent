using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class BCascaderList<TItem, TValue> : BSelectList<TItem, TValue, TValue>
    {
        [Parameter]
        public EventCallback<TItem> OnItemClick { get; set; }

        [Parameter]
        public List<TItem> Children { get; set; }
    }
}

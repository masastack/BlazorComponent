using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCascaderMenuBody<TItem, TValue, TInput> : BSelectMenu<TItem, TValue, TInput>
        where TInput : ICascader<TItem, TValue>
    {
        protected bool ShowChildren { get; set; }

        protected List<TItem> Children { get; set; }

        public Func<TItem, List<TItem>> ItemChildren => Component.ItemChildren;

        [Parameter]
        public List<TItem> Items { get; set; }

        public EventCallback<TItem> HandleOnItemClick => EventCallback.Factory.Create<TItem>(this, item =>
        {
            var children = ItemChildren(item);

            if (children != null && children.Count > 0)
            {
                ShowChildren = true;
                Children = children;
            }
            else
            {
                ShowChildren = false;
            }
        });
    }
}

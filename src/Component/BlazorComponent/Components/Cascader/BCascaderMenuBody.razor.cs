using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCascaderMenuBody<TItem, TValue, TInput> : BSelectMenu<TItem, TValue, TValue, TInput>
        where TInput : ICascader<TItem, TValue>
    {
        protected bool ShowChildren { get; set; }

        protected List<TItem> Children { get; set; }

        protected Func<TItem, List<TItem>> ItemChildren => Component.ItemChildren;

        protected Func<TItem, Task> LoadChildren => Component.LoadChildren;

        protected TItem LoadingItem { get; set; }

        [Parameter]
        public List<TItem> Items { get; set; }

        protected StringBoolean IsLoading(TItem item)
        {
            return LoadChildren == null ? null : EqualityComparer<TItem>.Default.Equals(item, LoadingItem);
        }

        //TODO: refactor this
        public EventCallback<TItem> HandleOnItemClick => EventCallback.Factory.Create<TItem>(this, async item =>
        {
            var children = ItemChildren(item);

            if (LoadChildren != null && children != null && children.Count == 0)
            {
                LoadingItem = item;
                await LoadChildren(item);
                LoadingItem = default;

                children = ItemChildren(item);
            }

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

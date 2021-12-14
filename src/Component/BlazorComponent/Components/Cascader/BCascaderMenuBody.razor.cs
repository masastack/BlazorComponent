using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;

namespace BlazorComponent
{
    public partial class BCascaderMenuBody<TItem, TValue, TInput> : BSelectMenu<TItem, TValue, TValue, TInput>
        where TInput : ICascader<TItem, TValue>
    {
        protected bool ShowChildren => Component.ChildrenItems.TryGetValue(Level, out _);

        protected List<TItem> Children => Component.ChildrenItems[Level];

        protected Func<TItem, List<TItem>> ItemChildren => Component.ItemChildren;

        protected Func<TItem, Task> LoadChildren => Component.LoadChildren;

        [Parameter]
        public List<TItem> Items { get; set; }

        [Parameter]
        public int Level { get; set; }

        protected StringBoolean IsLoading(TItem item)
        {
            return LoadChildren == null ? null : EqualityComparer<TItem>.Default.Equals(item, Component.LoadingItem);
        }

        public EventCallback<TItem> HandleOnItemClick => CreateEventCallback<TItem>(async item => await Component.HandleOnItemClickAsync(item, Level));
    }
}

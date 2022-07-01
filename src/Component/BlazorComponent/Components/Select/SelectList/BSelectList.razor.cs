using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;

namespace BlazorComponent
{
    public partial class BSelectList<TItem, TItemValue, TValue> : BDomComponentBase
    {
        private static Func<TItem, string> _itemHeader;
        private static Func<TItem, bool> _itemDivider;

        [Parameter]
        public bool HideSelected { get; set; }

        [EditorRequired]
        [Parameter]
        public IList<TItem> Items { get; set; }

        [Parameter]
        public Func<TItem, bool> ItemDisabled { get; set; }

        [EditorRequired]
        [Parameter]
        public Func<TItem, string> ItemText { get; set; }

        [EditorRequired]
        [Parameter]
        public Func<TItem, TItemValue> ItemValue { get; set; }

        [Parameter]
        public string NoDataText { get; set; }

        [Parameter]
        public IEnumerable<TItem> SelectedItems { get; set; }

        [Parameter]
        public RenderFragment NoDataContent { get; set; }

        [Parameter]
        public RenderFragment PrependItemContent { get; set; }

        [Parameter]
        public RenderFragment AppendItemContent { get; set; }

        [Parameter]
        public int SelectedIndex { get; set; }

        protected IList<TItemValue> ParsedItems
        {
            get
            {
                return SelectedItems.Select(ItemValue).ToList();
            }
        }

        protected bool HasItem(TItem item)
        {
            return ParsedItems.IndexOf(ItemValue(item)) > -1;
        }

        protected Func<TItem, string> ItemHeader
        {
            get
            {
                if (_itemHeader == null)
                {
                    _itemHeader = GetFuncOrDefault<string>("Header");
                }

                return _itemHeader;
            }
        }

        private static Func<TItem, T> GetFuncOrDefault<T>(string name)
        {
            Func<TItem, T> func;
            try
            {
                var parameterExpression = Expression.Parameter(typeof(TItem), "item");
                var propertyExpression = Expression.Property(parameterExpression, name);

                var lambdaExpression = Expression.Lambda<Func<TItem, T>>(propertyExpression, parameterExpression);
                func = lambdaExpression.Compile();
            }
            catch
            {
                func = item => default;
            }

            return func;
        }

        protected Func<TItem, bool> ItemDivider
        {
            get
            {
                if (_itemDivider == null)
                {
                    _itemDivider = GetFuncOrDefault<bool>("Divider");
                }

                return _itemDivider;
            }
        }
    }
}

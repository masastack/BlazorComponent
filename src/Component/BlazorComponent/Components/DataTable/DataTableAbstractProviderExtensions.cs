using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class DataTableAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyDataTableDefault<TItem>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BDataIteratorDefaultSlot<,>), typeof(BDataTableDefaultSlot<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableCaption<,>), typeof(BDataTableCaption<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableColgroup<,>), typeof(BDataTableColgroup<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableHeaders<,>), typeof(BDataTableHeaders<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableBody<,>), typeof(BDataTableBody<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableFoot<,>), typeof(BDataTableFoot<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableFooters<,>), typeof(BDataTableFooters<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableLoading<,>), typeof(BDataTableLoading<TItem, IDataTable<TItem>>))
                .Apply(typeof(BLoadableProgress<>), typeof(BLoadableProgress<IDataTable<TItem>>))
                .Apply(typeof(BDataTableItems<,>), typeof(BDataTableItems<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableGroupedRows<,>), typeof(BDataTableGroupedRows<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableDefaultRows<,>), typeof(BDataTableDefaultRows<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableDefaultExpandedRow<,>), typeof(BDataTableDefaultExpandedRow<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableDefaultGroupedRow<,>), typeof(BDataTableDefaultGroupedRow<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableDefaultSimpleRow<,>), typeof(BDataTableDefaultSimpleRow<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableRows<,>), typeof(BDataTableRows<TItem, IDataTable<TItem>>))
                .Merge(typeof(BDataIteratorEmptyWrapper<,>), typeof(BDataTableEmptyWrapper<TItem, IDataTable<TItem>>))
                .Apply(typeof(BDataTableScopedRows<,>), typeof(BDataTableScopedRows<TItem, IDataTable<TItem>>));
        }
    }
}

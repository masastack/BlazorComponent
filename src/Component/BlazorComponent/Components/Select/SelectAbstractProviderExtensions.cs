using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class SelectAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplySelectDefault<TItem, TItemValue, TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BSelectDefaultSlot<TItem, TItemValue, TValue>))
                .Apply(typeof(BSelectHiddenInput<,,,>), typeof(BSelectHiddenInput<TItem, TItemValue, TValue, ISelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectMenu<,,,>), typeof(BSelectMenu<TItem, TItemValue, TValue, ISelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectSelections<,,,>), typeof(BSelectSelections<TItem, TItemValue, TValue, ISelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectChipSelection<,,,>), typeof(BSelectChipSelection<TItem, TItemValue, TValue, ISelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectSlotSelection<,,,>), typeof(BSelectSlotSelection<TItem, TItemValue, TValue, ISelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectCommaSelection<,,,>), typeof(BSelectCommaSelection<TItem, TItemValue, TValue, ISelect<TItem, TItemValue, TValue>>))
                .Apply(typeof(BSelectList<,,,>), typeof(BSelectList<TItem, TItemValue, TValue, ISelect<TItem, TItemValue, TValue>>));
        }
    }
}

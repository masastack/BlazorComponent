using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class SelectListAbstractProviderExtensitions
    {
        public static ComponentAbstractProvider ApplySelectListDefault<TItem, TItemValue, TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BSelectListTile<,,>), typeof(BSelectListTile<TItem, TItemValue, TValue>))
                .Apply(typeof(BSelectListAction<,,>), typeof(BSelectListAction<TItem, TItemValue, TValue>))
                .Apply(typeof(BSelectListTileContent<,,>), typeof(BSelectListTileContent<TItem, TItemValue, TValue>))
                .Apply(typeof(BSelectListStaticNoDataTile<,,>), typeof(BSelectListStaticNoDataTile<TItem, TItemValue, TValue>))
                .Apply(typeof(BSelectListDivider<,,>), typeof(BSelectListDivider<TItem, TItemValue, TValue>))
                .Apply(typeof(BSelectListHeader<,,>), typeof(BSelectListHeader<TItem, TItemValue, TValue>));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BCascaderList<TItem, TValue, TInput> where TInput : ICascader<TItem, TValue>
    {
        protected IList<TItem> ComputedItems => Component.ComputedItems;
    }
}

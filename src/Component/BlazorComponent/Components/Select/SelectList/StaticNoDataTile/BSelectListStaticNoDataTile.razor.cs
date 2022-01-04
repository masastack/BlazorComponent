using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectListStaticNoDataTile<TItem, TItemValue, TValue>
    {
        protected string NoDataText => Component.NoDataText;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTabsBody<TTabs> : ComponentPartBase<TTabs>
        where TTabs : ITabs
    {
        List<ITabItem> TabItems => Component.TabItems;

        StringNumber Value => Component.Value;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataIteratorFooter<TItem, TComponent> where TComponent : IDataIterator<TItem>
    {
        public bool HideDefaultFooter=>Component.HideDefaultFooter;
    }
}

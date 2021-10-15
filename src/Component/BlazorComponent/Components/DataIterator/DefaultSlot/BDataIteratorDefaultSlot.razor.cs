using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataIteratorDefaultSlot<TItem, TDataIterator> where TDataIterator : IDataIterator<TItem>
    {
        public RenderFragment HeaderContent => Component.HeaderContent;

        public RenderFragment FooterContent => Component.FooterContent;
    }
}

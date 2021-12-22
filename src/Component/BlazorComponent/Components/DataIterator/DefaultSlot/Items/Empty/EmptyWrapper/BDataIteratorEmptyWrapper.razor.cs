using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDataIteratorEmptyWrapper<TItem,TDataIterator>
        where TDataIterator:IDataIterator<TItem>
    {
        [Parameter]
        public RenderFragment ChildContent { get; set; }
    }
}

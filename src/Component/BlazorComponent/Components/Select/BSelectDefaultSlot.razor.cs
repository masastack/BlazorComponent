using BlazorComponent;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSelectDefaultSlot<TItem, TItemValue, TValue>
    {
        public string Prefix => Component.Prefix;

        public string Suffix => Component.Prefix;
    }
}

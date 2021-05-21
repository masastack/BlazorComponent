using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerBody<TComponent> : BDomComponentBase, IPickerBody where TComponent : BPicker
    {
        [Parameter]
        public TComponent Component { get; set; }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BColorPickerInput<TColorPickerEdit> where TColorPickerEdit : IColorPickerEdit
    {
        [Parameter]
        public string Target { get; set; }

        [Parameter]
        public Dictionary<string, object> Attrs { get; set; }
    }
}

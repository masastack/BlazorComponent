using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BPickerTitle<TPicker> where TPicker : IPicker
    {
        public RenderFragment TitleContent => Component.TitleContent;
    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputIcon<TInput> : ComponentAbstractBase<TInput>
        where TInput : IInput
    {
        [Parameter]
        public string Icon { get; set; }

        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public EventCallback<MouseEventArgs> OnClick{ get; set; }
    }
}

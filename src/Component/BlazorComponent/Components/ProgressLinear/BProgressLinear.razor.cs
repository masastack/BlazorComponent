using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BProgressLinear : BDomComponentBase
    {
        [Parameter]
        public bool Indeterminate { get; set; }

        public virtual Task HandleOnClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}

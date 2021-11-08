using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BStepperStep : BDomComponentBase
    {
        [Parameter]
        public EventCallback<MouseEventArgs> OnClick { get; set; }

        public virtual Task HandleOnClickAsync(MouseEventArgs args)
        {
            return Task.CompletedTask;
        }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BMessagesMessage<TMessages> where TMessages : IMessages
    {
        [Parameter]
        public string Message { get; set; }

        public RenderFragment<string> ComponentChildContent => Component.ChildContent;
    }
}

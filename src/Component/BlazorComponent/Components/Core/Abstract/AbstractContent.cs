using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class AbstractContent : ComponentBase
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            if (Name == null)
            {
                throw new ArgumentNullException(nameof(Name));
            }
        }
    }
}

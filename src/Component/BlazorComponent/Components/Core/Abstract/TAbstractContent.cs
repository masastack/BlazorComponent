using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class TAbstractContent<TContext> : ComponentBase, IAbstractContent
    {
        [Parameter]
        public string Name { get; set; }

        [Parameter]
        public RenderFragment<TContext> ChildContent { get; set; }

        protected override void OnParametersSet()
        {
            if (Name == null)
            {
                throw new ArgumentNullException(nameof(Name));
            }
        }
    }
}

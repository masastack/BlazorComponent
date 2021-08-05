using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputAppendSlot<TInput> where TInput : IInput
    {
        [Parameter]
        public TInput Input { get; set; }

        public string AppendIcon => Input.AppendIcon;

        public RenderFragment AppendContent => Input.AppendContent;

        public ComponentCssProvider CssProvider => Input.CssProvider;

        public ComponentAbstractProvider AbstractProvider => Input.AbstractProvider;

        protected override void OnParametersSet()
        {
            if (Input == null)
            {
                throw new ArgumentNullException(nameof(Input));
            }
        }
    }
}

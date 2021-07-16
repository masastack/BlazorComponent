using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BListGroup : BDomComponentBase
    {
        private const string PREPEND = "prepend";
        private const string APPEND = "append";

        [Parameter]
        public string PrependIcon { get; set; }

        [Parameter]
        public string AppendIcon { get; set; }

        [Obsolete("Use ActivatorContent instead.")]
        [Parameter]
        public RenderFragment Activator { get; set; }

        [Parameter]
        public RenderFragment ActivatorContent { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        protected bool IsActive { get; set; }

        protected override void OnParametersSet()
        {
            base.OnParametersSet();

            if (Activator != null)
            {
                ActivatorContent = Activator;
            }
        }

        public void DeActive()
        {
            IsActive = false;
        }
    }
}

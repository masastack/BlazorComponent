using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDialogActivator<TDialog> where TDialog : IDialog
    {
        public RenderFragment<ActivatorProps> ActivatorContent => Component.ActivatorContent;

        public Dictionary<string, object> ActivatorAttrs => Component.ActivatorAttrs;
    }
}

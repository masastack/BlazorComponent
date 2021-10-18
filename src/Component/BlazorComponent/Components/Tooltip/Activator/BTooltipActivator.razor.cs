using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTooltipActivator<TTooltip> where TTooltip : ITooltip
    {
        protected Guid ActivatorId => Component.ActivatorId;

        public RenderFragment<ActivatorProps> ActivatorContent => Component.ActivatorContent;

        public Dictionary<string, object> ActivatorAttrs => Component.ActivatorAttrs;

        public bool Value => Component.Value;

        public EventCallback<bool> ValueChanged => Component.ValueChanged;
    }
}

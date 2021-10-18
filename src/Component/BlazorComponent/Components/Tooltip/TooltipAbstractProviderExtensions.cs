using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class TooltipAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyTooltipDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BTooltipContent<>), typeof(BTooltipContent<ITooltip>))
                .Apply(typeof(BTooltipActivator<>), typeof(BTooltipActivator<ITooltip>));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class OverlayAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyOverlayDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BOverlayScrim<>), typeof(BOverlayScrim<IOverlay>))
                .Apply(typeof(BOverlayContent<>), typeof(BOverlayContent<IOverlay>));
        }
    }
}

using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Extensions
{
    public static class MixinsExtensions
    {
        public static int ComputedScrollThreshold(this IScrollable scrollable) {
            return scrollable.ScrollThreshold ==0 ? 100 : scrollable.ScrollThreshold;
        }
    }
}

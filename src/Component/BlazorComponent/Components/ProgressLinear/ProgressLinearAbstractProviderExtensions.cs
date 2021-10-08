using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class ProgressLinearAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyProgressLinearDefault(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .Apply(typeof(BProgressLinearStream<>), typeof(BProgressLinearStream<IProgressLinear>))
                .Apply(typeof(BProgressLinearBackground<>), typeof(BProgressLinearBackground<IProgressLinear>))
                .Apply(typeof(BProgressLinearBuffer<>), typeof(BProgressLinearBuffer<IProgressLinear>))
                .Apply(typeof(BProgressLinearLong<>), typeof(BProgressLinearLong<IProgressLinear>))
                .Apply(typeof(BProgressLinearShort<>), typeof(BProgressLinearShort<IProgressLinear>))
                .Apply(typeof(BProgressLinearIndeterminate<>), typeof(BProgressLinearIndeterminate<IProgressLinear>))
                .Apply(typeof(BProgressLinearDeterminate<>), typeof(BProgressLinearDeterminate<IProgressLinear>))
                .Apply(typeof(BProgressLinearContent<>), typeof(BProgressLinearContent<IProgressLinear>));
        }
    }
}

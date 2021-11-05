using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class InputAbstractProviderExtensions
    {
        public static ComponentAbstractProvider ApplyInputDefault<TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                 .Apply(typeof(BInputContent<,>), typeof(BInputContent<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputPrependSlot<,>), typeof(BInputPrependSlot<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputSlot<,>), typeof(BInputSlot<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputIcon<,>), typeof(BInputIcon<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputControl<,>), typeof(BInputControl<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputInputSlot<,>), typeof(BInputInputSlot<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputDefaultSlot<,>), typeof(BInputDefaultSlot<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputLabel<,>), typeof(BInputLabel<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputMessages<,>), typeof(BInputMessages<TValue, IInput<TValue>>))
                 .Apply(typeof(BInputAppendSlot<,>), typeof(BInputAppendSlot<TValue, IInput<TValue>>));
        }

        public static ComponentAbstractProvider ApplyInputPrependIcon(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BIcon), type, "prepend-icon", propertiesAction);
        }

        public static ComponentAbstractProvider ApplyInputLabel(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BLabel), type, propertiesAction);
        }

        public static ComponentAbstractProvider ApplyInputMessages(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BMessages), type, propertiesAction);
        }

        public static ComponentAbstractProvider ApplyInputAppendIcon(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BIcon), type, "append-icon", propertiesAction);
        }
    }
}

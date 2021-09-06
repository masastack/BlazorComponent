using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class TextFieldExtensions
    {
        public static ComponentAbstractProvider ApplyTextFieldDefault<TValue>(this ComponentAbstractProvider abstractProvider)
        {
            return abstractProvider
                .ApplyTextFieldDefault<TValue>()
                .Apply(typeof(BTextFieldAffix<,>), typeof(BTextFieldAffix<TValue, ITextField<TValue>>))
                .Merge(typeof(BInputAppendSlot<,>), typeof(BTextFieldAppendSlot<TValue>))
                .Apply(typeof(BTextFieldClearIcon<,>), typeof(BTextFieldClearIcon<TValue, ITextField<TValue>>))
                .Apply(typeof(BTextFieldCounter<,>), typeof(BTextFieldCounter<TValue, ITextField<TValue>>))
                .Merge(typeof(BInputDefaultSlot<,>), typeof(BTextFieldDefaultSlot<TValue, ITextField<TValue>>))
                .Apply(typeof(BTextFieldFieldset<,>), typeof(BTextFieldFieldset<TValue, ITextField<TValue>>))
                .Apply(typeof(BTextFieldIconSlot<,>), typeof(BTextFieldIconSlot<TValue, ITextField<TValue>>))
                .Apply(typeof(BTextFieldInput<,>), typeof(BTextFieldInput<TValue, ITextField<TValue>>))
                .Merge(typeof(BInputInputSlot<,>), typeof(BTextFieldInputSlot<TValue>))
                .Apply(typeof(BTextFieldLegend<,>), typeof(BTextFieldLegend<TValue, ITextField<TValue>>))
                .Merge(typeof(BInputMessages<,>), typeof(BTextFieldMessages<TValue>))
                .Apply(typeof(BTextFieldPrependInnerSlot<,>), typeof(BTextFieldPrependInnerSlot<TValue, ITextField<TValue>>))
                .Apply(typeof(BTextFieldProgress<,>), typeof(BTextFieldProgress<TValue, ITextField<TValue>>))
                .Apply(typeof(BTextFieldTextFieldSlot<,>), typeof(BTextFieldTextFieldSlot<TValue, ITextField<TValue>>))
                .Merge(typeof(BInputLabel<,>), typeof(BTextFieldLabel<TValue>));
        }

        public static ComponentAbstractProvider ApplyTextFieldPrependIcon(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BIcon), type, "prepend-icon", propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldLabel(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Merge(typeof(BLabel), type, propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldMessages(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BMessages), type, propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldAppendIcon(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BIcon), type, "append-icon", propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldCounter(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BCounter), type, propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldProcessLinear(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BProcessLinear), type, propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldClearIcon(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BIcon), type, "clear-icon", propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldAppendOuterIcon(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BIcon), type, "append-outer-icon", propertiesAction);
        }

        public static ComponentAbstractProvider ApplyTextFieldPrependInnerIcon(this ComponentAbstractProvider abstractProvider, Type type, Action<Dictionary<string, object>> propertiesAction = null)
        {
            return abstractProvider
                .Apply(typeof(BIcon), type, "prepend-inner-icon", propertiesAction);
        }
    }
}

using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BTextFieldTextFieldSlot<TValue, TInput> where TInput : ITextField<TValue>
    {
        public string Prefix => Component.Prefix;

        public string Suffix => Component.Suffix;

        public Action<ElementReference> PrefixReferenceCapture => element => Component.PrefixElement = element;

    }
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent
{
    public partial class BTextFieldClearIcon<TValue, TInput> where TInput : ITextField<TValue>
    {
        public bool Clearable => Component.Clearable;

        public bool IsDirty => Component.IsDirty;

        public virtual string ClearIcon => Component.ClearIcon;

        public EventCallback<MouseEventArgs> HandleOnClearClickAsync => EventCallback.Factory.Create<MouseEventArgs>(Component, Component.HandleOnClearClickAsync);
    }
}

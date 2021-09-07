using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

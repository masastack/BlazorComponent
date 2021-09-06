using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    /// <summary>
    /// &lt;div class="@CssProvider.GetClass(ComputedType)"&gt;<br/>
    /// --@ChildContent<br/>
    /// &lt;/div"&gt;
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TInput"></typeparam>
    public partial class BTextFieldAffix<TValue, TInput> where TInput : ITextField<TValue>
    {
        [Parameter]
        public string Type { get; set; }

        [Parameter]
        public RenderFragment ChildContent { get; set; }

        public virtual string ComputedType => $"text-field-{Type}";
    }
}

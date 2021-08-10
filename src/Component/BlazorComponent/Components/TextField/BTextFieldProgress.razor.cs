using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldProgress<TValue, TInput> where TInput : ITextField<TValue>
    {
        public StringBoolean Loading => Component.Loading;

        public RenderFragment ProgressContent => Component.ProgressContent;
    }
}

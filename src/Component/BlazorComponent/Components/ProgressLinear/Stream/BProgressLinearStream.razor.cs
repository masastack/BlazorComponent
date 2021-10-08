using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace BlazorComponent
{
    public partial class BProgressLinearStream<TProgressLinear> where TProgressLinear : IProgressLinear
    {
        public bool Stream => Component.Stream;
    }
}

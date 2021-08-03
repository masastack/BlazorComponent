using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputMessages<TInput> where TInput : BInput
    {
        [Parameter]
        public TInput Input { get; set; }

        public bool ShowDetails => Input.ShowDetails;

        public ComponentAbstractProvider AbstractProvider => Input.AbstractProvider;
    }
}

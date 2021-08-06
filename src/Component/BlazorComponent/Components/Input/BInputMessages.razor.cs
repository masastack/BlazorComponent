using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BInputMessages<TInput> where TInput : IInput
    {
        public bool ShowDetails => Component.ShowDetails;
    }
}

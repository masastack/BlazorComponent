using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BPickerBodyTransition<TPicker> where TPicker : IPicker
    {
        public string Transition => Component.Transition;

        public bool NoTitle=>Component.NoTitle;

        public RenderFragment ComponentChildContent => Component.ChildContent;
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BOverlayContent<TOverlay> where TOverlay : IOverlay
    {
        public bool Value => Component.Value;

        public RenderFragment ComponentChildContent =>Component.ChildContent;
    }
}

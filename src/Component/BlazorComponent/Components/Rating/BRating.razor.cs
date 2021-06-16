using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BRating : BDomComponentBase
    {
        [Parameter]
        public double Value { get; set; }

        [Parameter]
        public EventCallback<double> ValueChanged { get; set; }

        [Parameter]
        public double Length { get; set; } = 5;

        [Parameter]
        public bool HalfIncrements { get; set; } = false;

        [Parameter]
        public bool Hover { get; set; }

        [Parameter]
        public string Color { get; set; }

        [Parameter]
        public string BackgroundColor { get; set; }

        [Parameter]
        public string EmptyIcon { get; set; } = "mdi-star-outline";

        [Parameter]
        public string FullIcon { get; set; } = "mdi-star";

        [Parameter]
        public string HalfIcon { get; set; } = "mdi-star-half-full";

        [Parameter]
        public string Size { get; set; }

        [Parameter]
        public bool Small { get; set; }

        [Parameter]
        public bool XSmall { get; set; }

        [Parameter]
        public bool XLarge { get; set; }

        [Parameter]
        public bool Large { get; set; }

        protected virtual async Task HandleClick(double i)
        {
            if (ValueChanged.HasDelegate)
            {
                await ValueChanged.InvokeAsync(i);
            }
            else
            {
                Value = i;
            }
        }
    }
}

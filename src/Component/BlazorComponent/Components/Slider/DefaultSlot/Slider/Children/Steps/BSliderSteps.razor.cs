using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BSliderSteps<TInput> where TInput : ISlider
    {
        public double Step => Component.Step;

        public bool ShowTicks => Component.ShowTicks;

        public double NumTicks => Component.NumTicks;

        public bool Vertical => Component.Vertical;

        public List<string> TickLabels => Component.TickLabels;
    }
}

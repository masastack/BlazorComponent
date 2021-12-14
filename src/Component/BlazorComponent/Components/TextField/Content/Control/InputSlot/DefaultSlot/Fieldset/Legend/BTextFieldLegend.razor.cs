using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldLegend<TValue, TInput> where TInput : ITextField<TValue>
    {
        protected string InnerHTML => Component.LegendInnerHTML;
    }
}

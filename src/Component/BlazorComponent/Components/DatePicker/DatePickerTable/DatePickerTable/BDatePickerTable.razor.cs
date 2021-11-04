using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BDatePickerTable : BDomComponentBase
    {
        [Parameter]
        public virtual DateOnly TableDate { get; set; }

        protected virtual string ComputedTransition { get; }
    }
}

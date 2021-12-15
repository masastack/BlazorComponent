using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldLabel<TValue>
    {
        public bool ShowLabel => Component.ShowLabel;

        public BLabel LabelReference
        {
            set
            {
                Component.LabelReference = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldMessages<TValue>
    {
        public ComponentCssProvider CssProvider => Input.CssProvider;

        public bool HasCounter => Input.HasCounter;
    }
}

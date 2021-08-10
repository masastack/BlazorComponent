using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldTextFieldSlot<TValue, TInput> where TInput : ITextField<TValue>
    {
        public string Prefix => Component.Prefix;

        public string Suffix => Component.Suffix;
    }
}

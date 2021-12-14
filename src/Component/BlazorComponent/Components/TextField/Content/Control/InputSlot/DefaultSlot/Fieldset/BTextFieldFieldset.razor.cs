using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BTextFieldFieldset<TValue, TInput> where TInput : ITextField<TValue>
    {
        public bool Outlined => Component.Outlined;
    }
}

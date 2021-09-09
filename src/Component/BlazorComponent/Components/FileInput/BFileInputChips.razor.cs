using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BFileInputChips<TValue, TInput> where TInput : IFileInput<TValue>
    {
        public bool IsDirty => Component.IsDirty;

        public IList<string> Text => Component.Text;
    }
}

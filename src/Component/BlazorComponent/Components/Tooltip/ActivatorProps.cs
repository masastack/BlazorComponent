using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ActivatorProps
    {
        public ActivatorProps(Dictionary<string, object> attrs)
        {
            Attrs = attrs;
        }

        public Dictionary<string, object> Attrs { get; }
    }
}
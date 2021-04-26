using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class SlotComponentDescription
    {
        public SlotComponentDescription(Type type)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
        }

        public SlotComponentDescription(Type type, Dictionary<string, object> properties)
            : this(type)
        {
            Properties = properties ?? throw new ArgumentNullException(nameof(properties));
        }

        public Type Type { get; }

        public Dictionary<string, object> Properties { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Web
{
    public class Event
    {
        public Event(string type, string name)
        {
            Type = type;
            Name = name;
        }

        internal string Type { get; }

        internal string Name { get; }

        internal bool ShouldStopPropagation { get; private set; }

        public void StopPropagation()
        {
            ShouldStopPropagation = true;
        }
    }
}

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

        public string Type { get; }

        public string Name { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class EventListenerOptions
    {
        public bool Capture { get; set; }

        public bool Once { get; set; }

        public bool Passive { get; set; }
    }
}

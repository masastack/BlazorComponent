using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Web
{
    public class MouseEvent : Event
    {
        public MouseEvent(string name)
            : base("MouseEvents", name)
        {
        }
    }
}

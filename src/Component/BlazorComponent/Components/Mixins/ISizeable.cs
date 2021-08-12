using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ISizeable
    {
        public bool XSmall { get; set; }

        public bool Small { get; set; }

        public bool Large { get; set; }

        public bool XLarge { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ItemContext
    {
        public bool IsActive { get; set; }

        public Action Toggle { get; set; }
    }
}

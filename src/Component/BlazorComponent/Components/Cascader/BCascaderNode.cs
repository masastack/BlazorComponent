using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class BCascaderNode
    {
        public string Value { get; set; }

        public string Label { get; set; }

        public List<BCascaderNode> Children { get; set; }
    }
}

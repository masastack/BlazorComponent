using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class AttributesDictionary : Dictionary<string, object>
    {
        public AttributesDictionary()
        {
        }

        public AttributesDictionary(int index)
        {
            Index = index;
        }

        public AttributesDictionary(object data)
        {
            Data = data;
        }

        public int Index { get; }

        public object Data { get; }
    }
}

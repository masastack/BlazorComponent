using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class PropsDictionary : Dictionary<string, object>
    {
        public PropsDictionary()
        {
        }

        public PropsDictionary(int index)
        {
            Index = index;
        }

        public PropsDictionary(object data)
        {
            Data = data;
        }

        public int Index { get; }

        public object Data { get; }
    }
}

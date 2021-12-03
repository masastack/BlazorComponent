using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ThemeOptions
    {
        public string CombinePrefix { get; set; }

        public string Primary { get; set; }

        public string Secondary { get; set; }

        public string Accent { get; set; }

        public string Error { get; set; }

        public string Info { get; set; }

        public string Success { get; set; }

        public string Warning { get; set; }

        public Dictionary<string, string> UserDefined { get; set; }
    }
}

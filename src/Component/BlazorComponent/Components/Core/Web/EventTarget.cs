using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Components.Web
{
    public class EventTarget
    {
        public string Id => GetAttribute("id");

        private string GetAttribute(string name)
        {
            if (Attributes != null && Attributes.TryGetValue(name, out var value))
            {
                return value;
            }

            return string.Empty;
        }

        public string Class => GetAttribute("class");

        public string Style => GetAttribute("style");

        public Dictionary<string, string> Attributes { get; set; }
    }
}

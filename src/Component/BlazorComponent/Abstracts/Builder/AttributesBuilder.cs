using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class AttributesBuilder
    {
        private Dictionary<string, object> _attributes = new();

        public AttributesBuilder Add(string name, object value)
        {
            _attributes.Add(name, value);
            return this;
        }

        public AttributesBuilder AddChildContent(RenderFragment childContent)
        {
            _attributes.Add("ChildContent", childContent);
            return this;
        }

        public IDictionary<string, object> Attributes
        {
            get
            {
                return _attributes;
            }
        }

        public AttributesBuilder SetAttributes(Dictionary<string, object> attributes)
        {
            //We may combine this with other attributes
            _attributes = attributes;
            return this;
        }
    }
}

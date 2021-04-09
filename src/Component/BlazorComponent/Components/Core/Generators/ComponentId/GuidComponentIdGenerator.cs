using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Components.Core.Generators.ComponentId
{
    public class GuidComponentIdGenerator : IComponentIdGenerator
    {
        public string Prefix { get; set; }

        public string Generate(BDomComponentBase component) => $"{Prefix ?? "B-" }{Guid.NewGuid()}";
    }
}

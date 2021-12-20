using BlazorComponent.Web;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Mixins
{
    public interface IDependent
    {
        void RegisterChild(IDependent dependent);

        IEnumerable<HtmlElement> DependentElements { get; }
    }
}

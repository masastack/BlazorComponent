using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IComponentPart
    {
        RenderFragment Content { get; }

        void Attach(IHasProviderComponent component);

        void SetParametersView(ParameterView parameterView);
    }
}

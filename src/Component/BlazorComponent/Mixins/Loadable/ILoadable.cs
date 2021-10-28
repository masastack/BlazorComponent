using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ILoadable : IHasProviderComponent
    {
        public StringBoolean Loading { get; }

        public StringNumber LoaderHeight { get; }

        public RenderFragment ProgressContent { get; }
    }
}

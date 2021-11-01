using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface ICard : IHasProviderComponent, ILoadable, ISheet
    {
        string Tag { get; }
    }
}

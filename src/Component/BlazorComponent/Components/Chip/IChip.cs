using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IChip : IHasProviderComponent
    {
        RenderFragment ChildContent { get; }

        bool Filter => default;

        string FilterIcon => default;

        bool IsActive => default;

        bool Close => default;

        string CloseIcon => default;
    }
}

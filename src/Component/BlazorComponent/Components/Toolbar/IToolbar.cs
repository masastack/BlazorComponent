using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IToolbar : ISheet
    {
        string Src { get; }

        RenderFragment<Dictionary<string, object>> ImgContent { get; }

        StringNumber Height { get; }

        bool IsExtended { get; }

        RenderFragment ExtensionContent { get; }
    }
}
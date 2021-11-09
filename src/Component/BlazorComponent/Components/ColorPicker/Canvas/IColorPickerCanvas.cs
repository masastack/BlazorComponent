using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public interface IColorPickerCanvas : IHasProviderComponent
    {
        Dictionary<string, object> CanvasAttrs { get; }

        ElementReference CanvasRef { set; }
    }
}

using Microsoft.AspNetCore.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public partial class BColorPickerCanvasCanvas<TColorPickerCanvas> where TColorPickerCanvas : IColorPickerCanvas
    {
        public Dictionary<string, object> CanvasAttrs => Component.CanvasAttrs;

        public ElementReference CanvasRef
        {
            set { Component.CanvasRef = value; }
        }
    }
}

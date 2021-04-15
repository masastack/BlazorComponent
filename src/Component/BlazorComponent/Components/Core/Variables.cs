using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class Variables
    {
        public static BreakpointTypes BreakpointTypes { get; set; } = new BreakpointTypes();

        public static string JsInteropFuncNamePrefix { get; set; } = "BlazorComponent.interop.";

        public static bool DarkTheme { get; set; }

        public static ThemeModel Theme { get; set; }
    }
}

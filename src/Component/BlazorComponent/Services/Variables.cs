namespace BlazorComponent
{
    public class Variables
    {
        public static BreakpointTypes BreakpointTypes { get; set; } = new BreakpointTypes();

        public static string JsInteropFuncNamePrefix { get; set; } = "BlazorComponent.interop.";

        public static bool DarkTheme { get; set; }

        public static ThemeOptions Theme { get; set; }
    }
}

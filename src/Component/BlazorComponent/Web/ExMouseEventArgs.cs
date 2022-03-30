namespace Microsoft.AspNetCore.Components.Web
{
    public class ExMouseEventArgs : MouseEventArgs
    {
        public EventTarget Target { get; set; }

        public double PageX { get; set; }

        public double PageY { get; set; }
    }
}

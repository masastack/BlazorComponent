namespace BlazorComponent
{
    public partial class BBorder : BDomComponentBase
    {
        [Parameter]
        [MasaApiParameter(Borders.Left)]
        public Borders Border { get; set; } = Borders.Left;

        [Parameter]
        public RenderFragment? ChildContent { get; set; }

        [Parameter]
        [MasaApiParameter("primary")]
        public string? Color { get; set; } = "primary";

        [Parameter]
        public bool Offset { get; set; }

        [Parameter]
        public StringBoolean? Rounded { get; set; }

        [Parameter]
        public string? WrapperStyle { get; set; }

        [Parameter]
        [MasaApiParameter(true)]
        public bool Value { get; set; } = true;

        [Parameter]
        [MasaApiParameter(8)]
        public StringNumber Width { get; set; } = 8;

        protected bool Inactive => !Value || Border == Borders.None;

        protected (double borderWidth, string? unit) ComputedWidth()
        {
            var unit = "px";

            var (isNumber, number) = Width.TryGetNumber();
            if (isNumber) return (number / 2, unit);

            string[] units =
                {"vmin", "vmcx", "rem", "em", "vw", "vh", "ex", "ch", "px", "cm", "mm", "in", "pt", "pc", "%"};

            unit = units.FirstOrDefault(u => Width.AsT0.Contains(u));

            if (unit == null) return (0, null);

            return ((double.TryParse(Width.AsT0.Split(unit)[0], out number) ? number : 0) / 2, unit);
        }
    }
}
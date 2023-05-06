namespace BlazorComponent
{
    public class RatingItem
    {
        public int Index { get; set; }

        public double Value { get; set; }

        public bool IsFilled { get; set; }

        public bool? IsHalfFilled { get; set; }

        public bool IsHovered { get; set; }

        public bool? IsHalfHovered { get; set; }

        public Action<ExMouseEventArgs> Click { get; set; }
    }
}

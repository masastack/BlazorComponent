namespace BlazorComponent
{
    public class BMenuProps
    {
        public bool Visible { get; set; }

        public bool OffsetX { get; set; }

        public bool OffsetY { get; set; }

        public bool Top { get; set; }

        public bool Bottom { get; set; }

        public bool Left { get; set; }

        public bool Right { get; set; }

        public StringNumber NudgeTop { get; set; }

        public StringNumber NudgeBottom { get; set; }

        public StringNumber NudgeLeft { get; set; }

        public StringNumber NudgeRight { get; set; }

        public StringNumber NudgeWidth { get; set; }

        public StringNumber MaxHeight { get; set; } = 400;

        public StringNumber MinWidth { get; set; }
    }
}

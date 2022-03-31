namespace BlazorComponent
{
    public class MenuableDimensions
    {
        public MenuablePosition Activator { get; set; } = new MenuablePosition();
        public MenuablePosition Content { get; set; } = new MenuablePosition();
        public double RelativeYOffset { get; set; }
        public double OffsetParentLeft { get; set; }
    }
}

namespace BlazorComponent
{
    public partial class BResponsiveSizer<TResponsive>
        where TResponsive : IResponsive
    {
        public StringNumber AspectRatio => Component.AspectRatio;
    }
}
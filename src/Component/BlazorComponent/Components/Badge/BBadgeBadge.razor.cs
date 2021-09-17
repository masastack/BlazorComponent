namespace BlazorComponent
{
    public partial class BBadgeBadge<TBadge> where TBadge : IBadge
    {
        public string Transition => Component.Transition;
    }
}

namespace BlazorComponent
{
    public class ScrollYTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scroll-y-transition";
        }
    }
}

namespace BlazorComponent
{
    public class FadeTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "fade-transition";
        }
    }
}

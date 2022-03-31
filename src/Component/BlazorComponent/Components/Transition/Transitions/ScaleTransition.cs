namespace BlazorComponent
{
    public class ScaleTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "scale-transition";
        }
    }
}

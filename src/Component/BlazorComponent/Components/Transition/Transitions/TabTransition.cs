namespace BlazorComponent
{
    public class TabTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "tab-transition";
        }
    }
}

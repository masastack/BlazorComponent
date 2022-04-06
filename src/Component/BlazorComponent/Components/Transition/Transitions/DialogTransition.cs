namespace BlazorComponent
{
    public class DialogTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "dialog-transition";
        }
    }
}

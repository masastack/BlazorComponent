namespace BlazorComponent
{
    public class ExpandXTransition : ExpandTransition
    {
        protected override string SizeProp => "width";

        protected override void OnParametersSet()
        {
            Name = "expand-x-transition";
        }
    }
}

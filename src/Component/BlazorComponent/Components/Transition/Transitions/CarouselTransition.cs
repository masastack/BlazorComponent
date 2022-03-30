namespace BlazorComponent
{
    public class CarouselTransition : Transition
    {
        protected override void OnParametersSet()
        {
            Name = "carousel-transition";
        }
    }
}

namespace BlazorComponent;

public class ShowTransitionElement : ToggleableTransitionElement
{
    protected override string ComputedStyle
    {
        get
        {
            // Console.WriteLine($"LazyValue:{LazyValue}");
            if (!LazyValue)
            {
                return string.Join(";", base.ComputedStyle, "display:none");
            }

            return base.ComputedStyle;
        }
    }
}
namespace BlazorComponent;

public class ShowTransitionElement : ToggleableTransitionElement
{
    protected override string ComputedStyle
    {
        get
        {
            if (!LazyValue)
            {
                return string.Join(";", base.ComputedStyle, "display:none");
            }

            return base.ComputedStyle;
        }
    }
}
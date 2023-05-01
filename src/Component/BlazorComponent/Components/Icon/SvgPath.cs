namespace BlazorComponent;

public class SvgPath
{
    public string D { get; }

    public float Opacity { get; }

    public SvgPath(string d, float opacity = 1)
    {
        D = d;

        if (opacity is > 1 or < 0)
        {
            opacity = 1;
        }

        Opacity = opacity;
    }
}

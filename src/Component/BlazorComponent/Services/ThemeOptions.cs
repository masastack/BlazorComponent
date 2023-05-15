namespace BlazorComponent;

public class ThemeOptions
{
    public string CombinePrefix { get; set; } = "";

    public string Primary { get; set; } = "";

    public string Secondary { get; set; } = "";

    public string Accent { get; set; } = "";

    public string Error { get; set; } = "";

    public string Info { get; set; } = "";

    public string Success { get; set; } = "";

    public string Warning { get; set; } = "";

    public Dictionary<string, string>? UserDefined { get; set; }
}

public class Theme
{
    public Theme(bool dark, ThemeOptions lightTheme, ThemeOptions darkTheme)
    {
        Dark = dark;
        Themes = new Themes(lightTheme, darkTheme);
    }

    public Theme(Func<IServiceProvider, Task<bool>> dark2, ThemeOptions lightTheme, ThemeOptions darkTheme) : this(false, lightTheme, darkTheme)
    {
        Dark2 = dark2;
    }

    public bool Dark { get; set; }
    
    public Func<IServiceProvider, Task<bool>> Dark2 { get; set; }

    public Themes Themes { get; }
}

public class Themes
{
    public Themes(ThemeOptions light, ThemeOptions dark)
    {
        Light = light;
        Dark = dark;
    }

    public ThemeOptions Dark { get; }

    public ThemeOptions Light { get; }
}

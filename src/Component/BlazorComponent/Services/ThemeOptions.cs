namespace BlazorComponent;

public class Palette
{
    public virtual string? CombinePrefix { get; set; }

    public virtual string? Primary { get; set; }

    public virtual string? Secondary { get; set; }

    public virtual string? Accent { get; set; }

    public virtual string? Error { get; set; }

    public virtual string? Info { get; set; }

    public virtual string? Success { get; set; }

    public virtual string? Warning { get; set; }

    public virtual Dictionary<string, string> UserDefined { get; } = new();
}

public class Theme(Palette darkPalette, Palette lightPalette)
{
    public Palette DarkPalette { get; set; } = darkPalette;

    public Palette LightPalette { get; set; } = lightPalette;
}
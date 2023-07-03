namespace BlazorComponent;

public class Router(IRoutable routable) : IRoutable
{
    public IDictionary<string, object?> Attributes { get; set; } = routable.Attributes;

    public bool Disabled { get; set; } = routable.Disabled;

    public string? Href { get; set; } = routable.Href;

    public bool Link { get; set; } = routable.Link;

    public EventCallback<MouseEventArgs> OnClick { get; set; } = routable.OnClick;

    public string? Tag { get; set; } = routable.Tag;

    public string? Target { get; set; } = routable.Target;

    public bool Exact { get; set; } = routable.Exact;

    public string? MatchPattern { get; set; } = routable.MatchPattern;

    public NavigationManager NavigationManager { get; set; } = routable.NavigationManager;
}

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public class Router : IRoutable
{
    public Router()
    {
    }

    public Router(IRoutable routable)
    {
        Attributes = routable.Attributes;
        Disabled = routable.Disabled;
        Href = routable.Href;
        Link = routable.Link;
        OnClick = routable.OnClick;
        Tag = routable.Tag;
        Target = routable.Target;
    }

    public IDictionary<string, object> Attributes { get; set; }

    public bool Disabled { get; set; }

    public string Href { get; set; }

    public bool Link { get; set; }

    public EventCallback<MouseEventArgs> OnClick { get; set; }

    public string Tag { get; set; }

    public string Target { get; set; }
}
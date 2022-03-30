using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorComponent;

public interface IRoutable
{
    IDictionary<string, object> Attributes { get; }

    bool Disabled { get; }

    string Href { get; }

    bool Link { get; }

    EventCallback<MouseEventArgs> OnClick { get; }

    string Tag { get; }

    string Target { get; }

    public bool IsClickable => !Disabled && (IsLink || OnClick.HasDelegate || Tabindex > 0);

    public bool IsLink => Href != null || Link;

    public int Tabindex => Attributes.TryGetValue("tabindex", out var tabindex) ? Convert.ToInt32(tabindex) : 0;

    public (string tag, Dictionary<string, object>) GenerateRouteLink()
    {
        string tag;
        Dictionary<string, object> attrs = new(Attributes);

        if (Href != null)
        {
            tag = "a";
            attrs["href"] = Href;
        }
        else
        {
            tag = Tag ?? "div";
        }

        if (Target != null)
        {
            attrs["target"] = Target;
        }

        return (tag, attrs);
    }
}
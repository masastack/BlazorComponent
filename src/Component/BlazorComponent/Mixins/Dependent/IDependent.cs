using BlazorComponent.Web;

namespace BlazorComponent.Mixins
{
    public interface IDependent
    {
        void RegisterChild(IDependent dependent);

        IEnumerable<HtmlElement> DependentElements { get; }
    }
}

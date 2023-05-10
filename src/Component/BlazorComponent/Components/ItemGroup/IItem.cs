namespace BlazorComponent
{
    public interface IItem : IGroupable
    {
        RenderFragment? ChildContent { get; set; }
    }
}
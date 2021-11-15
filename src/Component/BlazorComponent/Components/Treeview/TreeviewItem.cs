namespace BlazorComponent;

public class TreeviewItem<TItem>
{
    public TItem Item { get; set; }

    public bool Leaf { get; set; } 

    public bool Selected { get; set; }

    public bool Indeterminate { get; set; }

    public bool Active { get; set; }

    public bool Open { get; set; }
}
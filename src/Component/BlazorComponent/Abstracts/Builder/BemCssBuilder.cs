namespace BlazorComponent.Abstracts;

public class BemCssBuilder : CssBuilder
{
    private readonly IBlockOrElement _blockOrElement;
    private Func<IBlockOrElement, IBem>? _builder;

    public BemCssBuilder(IBlockOrElement blockOrElement, object? data = null)
    {
        _blockOrElement = blockOrElement;

        Data = data;
    }

    public CssBuilder Modifiers(Func<IBlockOrElement, IBem> bemBuilder)
    {
        _builder ??= bemBuilder;
        return this;
    }

    protected override List<string?> GetClassNames()
    {
        var classNames = base.GetClassNames();

        if (_builder != null)
        {
            // TODO: insert or add?
            // classNames.Insert(0, _builder.Invoke(_blockOrElement).Build());
            var className = _builder.Invoke(_blockOrElement).Build();
            classNames.Add(className);
        }

        return classNames;
    }
}

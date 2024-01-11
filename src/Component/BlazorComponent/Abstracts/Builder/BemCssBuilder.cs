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
        classNames.Add(_builder == null ? _blockOrElement.Name : _builder.Invoke(_blockOrElement).Build());
        return classNames;
    }
}

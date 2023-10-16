namespace BlazorComponent.Abstracts;

public class BemCssBuilder : CssBuilder
{
    private readonly IBlockOrElement _blockOrElement;
    private Func<IBlockOrElement, IBem> _builder;

    public BemCssBuilder(IBlockOrElement blockOrElement, object? data = null)
    {
        _blockOrElement = blockOrElement;
        _builder = be => be;

        Data = data;
    }

    public CssBuilder Modifiers(Func<IBlockOrElement, IBem> bemBuilder)
    {
        _builder = bemBuilder;
        return this;
    }

    protected override List<string?> GetClassNames()
    {
        var classNames = base.GetClassNames();
        classNames.Insert(0, _builder.Invoke(_blockOrElement).Build());
        return classNames;
    }
}

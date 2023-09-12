namespace BlazorComponent.Abstracts;

public class BemCssBuilder : CssBuilder
{
    private readonly IBlockOrElement _blockOrElement;
    private Func<IBlockOrElement, IBem> _builder;

    public BemCssBuilder(IBlockOrElement blockOrElement)
    {
        _blockOrElement = blockOrElement;
        _builder = be => be;
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

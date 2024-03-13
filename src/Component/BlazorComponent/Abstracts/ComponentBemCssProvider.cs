namespace BlazorComponent.Abstracts;

public class ComponentBemCssProvider
{
    private readonly Block _block;
    private readonly ComponentCssProvider _cssProvider;

    public ComponentBemCssProvider(
        ComponentCssProvider cssProvider,
        string block,
        Action<BemCssBuilder>? cssAction = null,
        Action<StyleBuilder>? styleAction = null)
    {
        _block = new Block(block);
        _cssProvider = cssProvider;

        Apply("default", BlockOrElement.Default, cssAction, styleAction);
    }

    public ComponentBemCssProvider Extend(string block, Action<BemCssBuilder>? cssAction = null, Action<StyleBuilder>? styleAction = null)
        => Apply(block, BlockOrElement.Block, cssAction, styleAction);

    public ComponentBemCssProvider Element(string element, Action<BemCssBuilder>? cssAction = null, Action<StyleBuilder>? styleAction = null)
        => Apply(element, BlockOrElement.Element, cssAction, styleAction);

    public ComponentBemCssProvider Apply(string name, Action<CssBuilder>? cssAction = null, Action<StyleBuilder>? styleAction = null)
    {
        _cssProvider.Apply(name, cssAction, styleAction);
        return this;
    }

    private ComponentBemCssProvider Apply(string name, BlockOrElement blockOrElement, Action<BemCssBuilder>? cssAction = null,
        Action<StyleBuilder>? styleAction = null)
    {
        _cssProvider.Apply(name, css =>
        {
            IBlockOrElement be = blockOrElement switch
            {
                BlockOrElement.Default => _block,
                BlockOrElement.Block   => _block.Extend(name),
                BlockOrElement.Element => _block.Element(name),
                _                      => throw new ArgumentOutOfRangeException(nameof(blockOrElement), blockOrElement, null)
            };

            var bemCssBuilder = new BemCssBuilder(be, css.Data);
            cssAction?.Invoke(bemCssBuilder);
            css.Add(bemCssBuilder.GetClass());
        }, styleAction);

        return this;
    }
}

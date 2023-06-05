namespace BlazorComponent;

public static class CssBuilderExtensions
{
    public static BuilderBase AddModifier(this CssBuilder builder, string modifier)
    {
        return builder.AddModifierIf(modifier, () => true);
    }

    public static BuilderBase AddModifier(this CssBuilder builder, Func<string> modifierFunc)
    {
        return builder.AddModifierIf(modifierFunc, () => true);
    }

    public static CssBuilder AddModifierIf(this CssBuilder builder, string modifier, Func<bool> conditionFunc)
    {
        builder.Mapper.TryAdd(() => $"{builder.Prefix}--{modifier}", conditionFunc);
        return builder;
    }

    public static CssBuilder AddModifierIf(this CssBuilder builder, Func<string> modifierFunc, Func<bool> conditionFunc)
    {
        builder.Mapper.TryAdd(() =>
        {
            var modifier = modifierFunc.Invoke();
            return $"{builder.Prefix}--{modifier}";
        }, conditionFunc);

        return builder;
    }
}

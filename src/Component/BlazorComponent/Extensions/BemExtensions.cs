using BemIt;

namespace BlazorComponent;

public static class BemExtensions
{
    public static IBem AddTheme(this IBem bem, bool isDark, bool exDark = false)
    {
        var dark = isDark || exDark;
        bem.AddClass(dark ? "theme--dark" : "theme--light");
        return bem;
    }

    public static IBem AddBackgroundColor(this IBem bem, string? color, bool apply = true)
    {
        return bem.AddColor(color, false, apply);
    }

    public static IBem AddTextColor(this IBem bem, string? color, bool apply = true)
    {
        return bem.AddColor(color, true, apply);
    }

    public static IBem AddColor(this IBem bem, string? color, bool isText, bool apply = true)
    {
        if (string.IsNullOrEmpty(color) || color.StartsWith("#") || color.StartsWith("rgb"))
        {
            return bem;
        }

        if (isText)
        {
            var colors = color.Split(" ");
            var firstColor = colors[0];

            if (apply)
            {
                bem.AddClass($"{firstColor}--text");
            }

            if (colors.Length == 2)
            {
                // TODO: 是否需要正则表达式验证格式
                // {darken|lighten|accent}-{1|2}

                var secondColor = colors[1];

                if (apply)
                {
                    bem.AddClass($"text--{secondColor}");
                }
            }
        }
        else
        {
            if (apply)
            {
                bem.AddClass(color);
            }
        }

        return bem;
    }
}

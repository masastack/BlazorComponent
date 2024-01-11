using System.Runtime.CompilerServices;
using System.Text;
using BemIt;

namespace BlazorComponent;

public static class BemExtensions
{
    public static Modifier AddOneOf(this Modifier modifier, 
        bool modifier1, bool modifier2,
        [CallerArgumentExpression("modifier1")] string name1 = "",
        [CallerArgumentExpression("modifier2")] string name2 = "")
    {
        if (modifier1)
        {
            modifier.Add(name1);
        }
        else if (modifier2)
        {
            modifier.Add(name2);
        }

        return modifier;
    }

    public static Modifier Add(this Modifier modifier, Enum @enum, bool apply = true)
    {
        if (apply)
        {
            return modifier.Add(@enum);
        }

        return modifier;
    }
    
    public static IBem AddTheme(this IBem bem, bool isDark, bool isIndependent = false)
    {
        bem.AddClass(isDark ? "theme--dark" : "theme--light");

        if (isIndependent)
        {
            bem.AddClass("theme--independent");
        }

        return bem;
    }

    public static IBem AddBackgroundColor(this IBem bem, string? color, bool apply = true)
    {
        return bem.AddColor(color, false, apply);
    }
    
    public static IBem AddElevation(this IBem bem, StringNumber? elevation)
    {
        if (elevation != null)
        {
            bem.AddClass($"elevation-{elevation}");
        }
        
        return bem;
    }
    
    public static IBem AddRounded(this IBem bem, StringBoolean? rounded, bool tile)
    {
        if (tile)
        {
            bem.AddClass("rounded-0");
        }
        else
        {
            if (rounded != null)
            {
                if (rounded.IsT0)
                {
                    var values = rounded.AsT0.Split(' ');

                    foreach (var val in values)
                    {
                        bem.AddClass($"rounded-{val}");
                    }
                }
                else if (rounded.IsT1 && rounded.AsT1)
                {
                    bem.AddClass("rounded");
                }
            }
        }

        return bem;
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

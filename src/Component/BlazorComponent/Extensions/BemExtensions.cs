using System.Runtime.CompilerServices;
using System.Text;
using BemIt;

namespace BlazorComponent;

public static class BemExtensions
{
    public static IBem AddTheme(this IBem bem, bool isDark, bool isIndependent = false)
    {
        bem.AddClass(CssClassUtils.GetTheme(isDark, isIndependent));
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

    public static IBem AddRounded(this IBem bem, StringBoolean? rounded, bool tile = false)
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
        if (apply)
        {
            bem.AddClass(CssClassUtils.GetColor(color, isText));
        }

        return bem;
    }
}

public static class CssClassUtils
{
    public static string? GetSize(bool xSmall, bool small, bool large, bool xLarge)
    {
        if (xSmall)
        {
            return "m-size--x-small";
        }
        
        if (small)
        {
            return "m-size--small";
        }
        
        if (large)
        {
            return "m-size--large";
        }
        
        if (xLarge)
        {
            return "m-size--x-large";
        }

        return "m-size--default";
    }
    
    public static string? GetColor(string? color, bool isText = false)
    {
        if (string.IsNullOrWhiteSpace(color) || color.StartsWith("#") || color.StartsWith("rgb"))
        {
            return null;
        }

        StringBuilder stringBuilder = new();

        if (isText)
        {
            var colors = color.Split(" ");
            var firstColor = colors[0];

            stringBuilder.Append($"{firstColor}--text ");

            if (colors.Length == 2)
            {
                // TODO: 是否需要正则表达式验证格式
                // {darken|lighten|accent}-{1|2}

                var secondColor = colors[1];
                stringBuilder.Append($"text--{secondColor} ");
            }
        }
        else
        {
            stringBuilder.Append(color);
        }

        return stringBuilder.Length > 0 ? stringBuilder.ToString().Trim() : null;
    }
    
    public static string? GetTheme(bool isDark, bool isIndependent = false)
    {
        StringBuilder stringBuilder = new();

        stringBuilder.Append(isDark ? "theme--dark " : "theme--light ");

        if (isIndependent)
        {
            stringBuilder.Append("theme--independent ");
        }

        return stringBuilder.Length > 0 ? stringBuilder.ToString().Trim() : null;
    }
}
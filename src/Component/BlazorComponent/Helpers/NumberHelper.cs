using System;
using System.Text.RegularExpressions;

namespace BlazorComponent;

public static class NumberHelper
{
    private static readonly Regex LeadingInteger = new(@"^(-?\d+)");
    private static readonly Regex LeadingDouble = new(@"^(-?\d+)(\.?\d+)");

    // TODO: test

    /// <summary>
    /// Same as parseInt in javascript. Return 0 if input is invalid.
    /// </summary>
    /// <param name="s"></param>
    /// <returns></returns>
    public static int ParseInt(string s)
    {
        var match = LeadingInteger.Match(s);
        return !match.Success ? 0 : int.Parse(match.Value);
    }

    // TODO: test

    /// <summary>
    /// Same as parseFloat in javascript. Return 0 if input is invalid.
    /// </summary>
    /// <param name="number"></param>
    /// <param name="s"></param>
    /// <returns></returns>
    public static double ParseDouble(string s)
    {
        var match = LeadingDouble.Match(s);
        return !match.Success ? 0 : double.Parse(match.Value);
    }

    public static bool TryParseDouble(string s, out double value)
    {
        value = 0;

        if (s == null) return false;

        var match = LeadingDouble.Match(s);

        if (!match.Success) return false;

        value = double.Parse(match.Value);

        return true;
    }
}
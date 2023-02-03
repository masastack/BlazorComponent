namespace BlazorComponent;

public static class StringExtensions
{
    public static string ReplaceFirst(this string str, string oldValue, string newValue)
    {
        if (string.IsNullOrEmpty(oldValue))
        {
            return str;
        }

        var index = str.IndexOf(oldValue, StringComparison.Ordinal);
        if (index == -1)
        {
            return str;
        }

        str = str.Remove(index, oldValue.Length);
        return str.Insert(index, newValue);
    }
}

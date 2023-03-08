namespace BlazorComponent
{
    public static class StringNumberExtensions
    {
        public static string ToUnit(this StringNumber? stringNumber, string unit = "px")
        {
            if (stringNumber == null)
            {
                return $"0{unit}";
            }

            return stringNumber.Match(
                t0 => t0,
                t1 => $"{t1}{unit}",
                t2 => $"{t2}{unit}"
            );
        }

        // TODO: ConvertToUnit更接近vuetify源码
        // TODO: 是否可以把上面的ToUnit删掉

        public static string? ConvertToUnit(this StringNumber? stringNumber, string unit = "px")
        {
            if (stringNumber == null)
            {
                return null;
            }

            return stringNumber.Match(
                t0 => string.IsNullOrWhiteSpace(t0) ? null : t0,
                t1 => $"{t1}{unit}",
                t2 => $"{t2}{unit}"
            );
        }
    }
}
using OneOf;

namespace BlazorComponent
{
    public class StringOrNumber : OneOfBase<string, int, double>
    {
        StringOrNumber(OneOf<string, int, double> _) : base(_) { }

        // optionally, define implicit conversions
        // you could also make the constructor public
        public static implicit operator StringOrNumber(string _) => new(_);
        public static implicit operator StringOrNumber(int _) => new(_);
        public static implicit operator StringOrNumber(double _) => new(_);

        public (bool isNumber, double number) TryGetNumber() =>
            Match(
                s => (double.TryParse(s, out var n), n),
                i => (true, i),
                d => (true, d)
            );
    }
}

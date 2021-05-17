using OneOf;

namespace BlazorComponent
{
    public class StringNumber : OneOfBase<string, int, double>
    {
        StringNumber(OneOf<string, int, double> _) : base(_) { }

        // optionally, define implicit conversions
        // you could also make the constructor public
        public static implicit operator StringNumber(string _) => new(_);
        public static implicit operator StringNumber(int _) => new(_);
        public static implicit operator StringNumber(double _) => new(_);

        public (bool isNumber, double number) TryGetNumber() =>
            Match(
                s => (double.TryParse(s, out var n), n),
                i => (true, i),
                d => (true, d)
            );
    }
}

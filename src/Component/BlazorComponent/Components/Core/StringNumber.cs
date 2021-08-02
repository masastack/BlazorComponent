using OneOf;
using System;

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

        public int ToInt32() => Match(
            t0 => int.TryParse(t0, out var val) ? val : 0,
            t1 => t1,
            t2 => Convert.ToInt32(t2)
            );

        public override string ToString()
        {
            return Value?.ToString();
        }

        public static bool operator ==(StringNumber left, StringNumber right)
        {
            if (Equals(left, right))
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Value == right.Value;
        }

        public static bool operator !=(StringNumber left, StringNumber right)
        {
            if (Equals(left, right))
            {
                return false;
            }

            if (left is null || right is null)
            {
                return true;
            }

            return left.Value != right.Value;
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}

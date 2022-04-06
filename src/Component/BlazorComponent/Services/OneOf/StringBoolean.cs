using OneOf;

namespace BlazorComponent
{
    public class StringBoolean : OneOfBase<string, bool>
    {
        StringBoolean(OneOf<string, bool> _) : base(_) { }

        public static implicit operator StringBoolean(string _) => new(_);
        public static implicit operator StringBoolean(bool _) => new(_);

        public static bool operator ==(StringBoolean left, StringBoolean right)
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

        public static bool operator !=(StringBoolean left, StringBoolean right)
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

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public bool IsString() => Match(
                t0 => true,
                t1 => false
                );
    }
}

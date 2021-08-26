using System;
using OneOf;

namespace BlazorComponent
{
    public class StringEnum<T> : OneOfBase<string, T> where T : Enum
    {
        StringEnum(OneOf<string, T> _) : base(_)
        {
        }

        public static implicit operator StringEnum<T>(string _) => new(_);

        public static implicit operator StringEnum<T>(T _) => new(_);

        public override string ToString()
        {
            return Value?.ToString();
        }

        public static bool operator ==(StringEnum<T> left, StringEnum<T> right)
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

        public static bool operator !=(StringEnum<T> left, StringEnum<T> right)
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
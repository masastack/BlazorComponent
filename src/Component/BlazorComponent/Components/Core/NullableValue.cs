using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class NullableValue<TValue>
    {
        public NullableValue(TValue value)
        {
            Value = value;
        }

        public TValue Value { get; set; }

        public static implicit operator NullableValue<TValue>(TValue value) => new(value);

        public static implicit operator TValue(NullableValue<TValue> value) => value == null ? default : value.Value;

        public override string ToString()
        {
            return Convert.ToString(Value);
        }

        public static bool operator ==(NullableValue<TValue> left, NullableValue<TValue> right)
        {
            if (Equals(left, right))
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return EqualityComparer<TValue>.Default.Equals(left.Value, right.Value);
        }

        public static bool operator !=(NullableValue<TValue> left, NullableValue<TValue> right)
        {
            if (Equals(left, right))
            {
                return false;
            }

            if (left is null || right is null)
            {
                return true;
            }

            return !EqualityComparer<TValue>.Default.Equals(left.Value, right.Value);
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

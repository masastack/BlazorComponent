using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}

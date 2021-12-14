using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class ListComparer
    {
        public static bool Equals<TValue>(IList<TValue> left, IList<TValue> right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            return left.All(right.Contains) && left.Count == right.Count;
        }
    }
}

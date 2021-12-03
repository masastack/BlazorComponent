using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class BreakpointTypes
    {
        public static readonly BreakpointType Xs = new(nameof(Xs).ToLowerInvariant(), 1, 600);

        public static readonly BreakpointType Sm = new(nameof(Sm), 2, 960);

        public static readonly BreakpointType Md = new(nameof(Md), 3, 1264);

        public static readonly BreakpointType Lg = new(nameof(Lg), 4, 1904);

        public static readonly BreakpointType Xl = new(nameof(Xl), 5, int.MaxValue - 1);

        public static readonly BreakpointType Xxl = new(nameof(Xxl), 6, int.MaxValue);
    }

    public record BreakpointType(string Name, int Value, int Width);
}

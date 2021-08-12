using BlazorComponent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent.Extensions
{
    public static class MixinsExtensions
    {
        public static bool Medium(this ISizeable sizeable)
        {
            return !sizeable.XSmall && !sizeable.Small && !sizeable.Large && !sizeable.XLarge;
        }

        public static string SizeClasses(this ISizeable sizeable)
        {
            return $"'v-size--x-small': {sizeable.XSmall}," +
                $"'v-size--small': {sizeable.Small}," +
                $"'v-size--default': {sizeable.Medium()}," +
                $"'v-size--large': {sizeable.Large}," +
                $"'v-size--x-large': {sizeable.XLarge},";
        }


        public static string ThemeClasses(this IThemeable themeable)
        {
            return themeable.IsDark ? "theme--dark" : "theme--light";
        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlazorComponent.Helpers
{
    public class RegexHelper
    {
        private const string svgPattern1 = @"^[mzlhvcsqta]\s*[-+.0-9][^mlhvzcsqta]+";
        private const string svgPattern2 = @"[\dz]$";

        /// <summary>
        /// return the str is it Svg Path
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool RegexSvgPath(string str)
        {
            var reg1 = new Regex(svgPattern1, RegexOptions.IgnoreCase);
            var reg2 = new Regex(svgPattern2, RegexOptions.IgnoreCase);
            return reg1.Match(str).Success && reg2.Match(str).Success && str.Length > 4;
        }
    }
}

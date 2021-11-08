using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class BuildBaseExtensions
    {
        public static string GetClass(this Dictionary<Func<string>, Func<bool>> mapper)
        {
            var classList = mapper.Where(i => i.Value() && !string.IsNullOrWhiteSpace(i.Key())).Select(i => i.Key()?.Trim());
            if (!classList.Any())
            {
                //In this case,style will never render as class="" but nothing
                return null;
            }

            return string.Join(" ", classList);
        }

        public static string GetStyle(this Dictionary<Func<string>, Func<bool>> mapper)
        {
            var styleList = mapper.Where(i => i.Value() && !string.IsNullOrWhiteSpace(i.Key())).Select(i => i.Key()?.Trim().Trim(';'));
            if (!styleList.Any())
            {
                //In this case,style will never render as style="" but nothing
                return null;
            }

            return string.Join(";", styleList);
        }
    }
}

using OneOf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public static class CalendarParser
    {
        public const string CalendarCategoryName = "name";
        public const string CalendarCategoryCategoryName = "categoryName";
        public const string CalendarCategoryKey = "key";

        public static List<OneOf<string, Dictionary<string, object>>> GetParsedCategories(
            OneOf<string, List<OneOf<string, Dictionary<string, object>>>> categories,
            OneOf<string, Func<OneOf<string, Dictionary<string, object>>, string>> categoryText)
        {
            var res = new List<OneOf<string, Dictionary<string, object>>>();
            if (categories.IsT0)
            {
                categories.AsT0?.Split(",").ForEach(x => {
                    res.Add(x);
                });

                return res;
            }

            foreach (var category in categories.AsT1)
            {
                if (category.IsT0)
                    res.Add(category);

                var categoryObj = category.AsT1;
                var categoryName = categoryObj != null && categoryObj.ContainsKey(CalendarCategoryCategoryName) ?
                    categoryObj[CalendarCategoryCategoryName].ToString() : ParsedCategoryText(category, categoryText);

                if(!string.IsNullOrWhiteSpace(categoryName))
                    res.Add(categoryName);

                res = TryGetCategoryValue(res, categoryObj, CalendarCategoryName);
                res = TryGetCategoryValue(res, categoryObj, CalendarCategoryCategoryName);
                res = TryGetCategoryValue(res, categoryObj, CalendarCategoryKey);
            }

            return res;
        }

        private static string ParsedCategoryText(
            OneOf<string, Dictionary<string, object>> category,
            OneOf<string, Func<OneOf<string, Dictionary<string, object>>, string>> categoryText)
        {
            if(categoryText.Match(t0 => string.IsNullOrWhiteSpace(t0), t1 => t1 == null))
                return null;

            return categoryText.IsT0 && category.IsT1 && category.AsT1.ContainsKey(categoryText.AsT0) ?
                category.AsT1[categoryText.AsT0].ToString() :
                (categoryText.IsT1 ? categoryText.AsT1(category) : category.AsT0);
        }

        private static List<OneOf<string, Dictionary<string, object>>> TryGetCategoryValue(
            List<OneOf<string, Dictionary<string, object>>> res, Dictionary<string, object> dic, string key)
        {
            if(res == null)
                res = new List<OneOf<string, Dictionary<string,object>>>();

            if (string.IsNullOrWhiteSpace(key) || dic == null || !dic.Any())
                return res;

            dic.TryGetValue(key, out var obj);
            if (obj != null)
                res.Add(obj.ToString());

            return res;
        }
    }
}

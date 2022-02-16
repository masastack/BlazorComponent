using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class UrlHelper
    {
        /// <summary>
        /// Determines whether url match currentUrl
        /// </summary>
        /// <param name="url"></param>
        /// <param name="currentUrl"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool Match(string url, string currentUrl)
        {
            if (url == null)
            {
                throw new ArgumentNullException(nameof(url));
            }

            if (currentUrl == null)
            {
                throw new ArgumentNullException(nameof(currentUrl));
            }

            if (string.Equals(url, currentUrl, StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }

            if (currentUrl.Length == url.Length - 1)
            {
                return url[^1] == '/'
                    && url.StartsWith(currentUrl, StringComparison.OrdinalIgnoreCase);
            }

            return false;
        }

        /// <summary>
        /// Determines whether currentUrl match prefix
        /// </summary>
        /// <param name="url"></param>
        /// <param name="currentUrl"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static bool MatchPrefix(string prefix, string currentUrl)
        {
            if (Match(prefix, currentUrl))
            {
                return true;
            }

            var prefixLength = prefix.Length;
            if (currentUrl.Length > prefixLength)
            {
                return currentUrl.StartsWith(prefix, StringComparison.OrdinalIgnoreCase)
                    && (prefixLength == 0
                        || !char.IsLetterOrDigit(prefix[prefixLength - 1])
                        || !char.IsLetterOrDigit(currentUrl[prefixLength])
                    );
            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorComponent
{
    public class PageTabItemManager
    {
        public static bool IsOpened(PageTabItem item)
        {
            return item.IsOpened;
        }

        public static DateTime OpenedTime(PageTabItem item)
        {
            return item.OpenedTime;
        }

        public static void Open(PageTabItem item)
        {
            if (item.IsOpened)
            {
                return;
            }

            item.IsOpened = true;
            item.OpenedTime = DateTime.Now;
        }

        public static void Close(PageTabItem item)
        {
            if (!item.IsOpened)
            {
                return;
            }

            item.IsOpened = false;
            item.OpenedTime = default;
        }
    }
}

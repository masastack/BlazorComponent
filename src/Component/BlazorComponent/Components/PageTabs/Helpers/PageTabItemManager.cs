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

            //Since items will order by OpenedTime
            //We want to keep it if reload 
            if (item.OpenedTime == default)
            {
                item.OpenedTime = DateTime.Now;
            }
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

        public static void Reload(PageTabItem item)
        {
            //Reload will not reset OpenedTime
            if (!item.IsOpened)
            {
                return;
            }

            item.IsOpened = false;
        }
    }
}

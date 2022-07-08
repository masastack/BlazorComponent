namespace BlazorComponent
{
    public class PageTabItem
    {
        public PageTabItem(string name, string url)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Url = url ?? throw new ArgumentNullException(nameof(url));

            if (Url.StartsWith("http") || url.StartsWith("https"))
            {
                AbsolutePath = new Uri(url).AbsolutePath;
            }
            else
            {
                AbsolutePath = Url.StartsWith("/") ? Url : "/" + Url;
            }
        }

        public PageTabItem(string name, string url, PageTabsMatch match)
            : this(name, url)
        {
            Match = match;
        }

        public PageTabItem(string name, string url, PageTabsMatch match, PageTabsTarget target)
            : this(name, url, match)
        {
            Target = target;
        }

        public PageTabItem(string name, string url, string icon)
            : this(name, url)
        {
            Icon = icon ?? throw new ArgumentNullException(nameof(icon));
        }

        public PageTabItem(string name, string url, string icon, PageTabsMatch match)
            : this(name, url, icon)
        {
            Match = match;
        }

        public PageTabItem(string name, string url, string icon, PageTabsMatch match, PageTabsTarget target)
            : this(name, url, icon, match)
        {
            Target = target;
        }

        public PageTabItem(string name, string url, string icon, bool closable)
            : this(name, url, icon)
        {
            Closable = closable;
        }

        public PageTabItem(string name, string url, string icon, bool closable, PageTabsMatch match)
            : this(name, url, icon, closable)
        {
            Match = match;
        }

        public PageTabItem(string name, string url, string icon, bool closable, PageTabsMatch match, PageTabsTarget target)
            : this(name, url, icon, closable, match)
        {
            Target = target;
        }

        public string Name { get; }

        public string Url { get; }

        public string Icon { get; }

        public bool Closable { get; } = true;

        public PageTabsMatch Match { get; } = PageTabsMatch.All;

        public PageTabsTarget Target { get; } = PageTabsTarget.Self;

        public string AbsolutePath { get; private set; }

        internal bool IsOpened { get; set; }

        internal DateTime OpenedTime { get; set; }
    }
}

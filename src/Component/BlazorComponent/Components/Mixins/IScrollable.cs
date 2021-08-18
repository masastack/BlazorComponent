namespace BlazorComponent
{
    public interface IScrollable
    {
        public string ScrollTarget { get; set; }
        
        public int ScrollThreshold { get; set; }

        int CurrentScroll { get; }

        int SavedScroll { get;  }

        public abstract void Scrolling();
    }
}
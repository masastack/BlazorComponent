namespace BlazorComponent
{
    public interface IScrollTarget
    {
        public string ScrollTarget { get; set; }
        
        public int ScrollThreshold { get; set; }
    }
}
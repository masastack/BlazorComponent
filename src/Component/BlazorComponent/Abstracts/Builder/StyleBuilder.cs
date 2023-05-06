namespace BlazorComponent
{
    public class StyleBuilder : BuilderBase
    {
        public string? Style => Mapper.GetStyle();
    }
}

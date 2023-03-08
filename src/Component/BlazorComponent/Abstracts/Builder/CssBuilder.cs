namespace BlazorComponent
{
    public class CssBuilder : BuilderBase
    {
        public string? Class => _mapper.GetClass();
    }
}

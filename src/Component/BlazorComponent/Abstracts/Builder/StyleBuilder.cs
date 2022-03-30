namespace BlazorComponent
{
    public class StyleBuilder : BuilderBase
    {
        public string Style => _mapper.GetStyle();
    }
}

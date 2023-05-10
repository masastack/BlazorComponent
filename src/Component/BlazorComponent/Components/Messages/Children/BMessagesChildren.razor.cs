namespace BlazorComponent
{
    public partial class BMessagesChildren<TMessages> where TMessages : IMessages
    {
        public List<string>? Value => Component.Value;
    }
}

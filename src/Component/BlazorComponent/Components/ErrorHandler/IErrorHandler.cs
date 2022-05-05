namespace BlazorComponent
{
    public interface IErrorHandler
    {
        Task HandleExceptionAsync(Exception exception);
    }
}

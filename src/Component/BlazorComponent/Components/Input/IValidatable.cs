namespace BlazorComponent
{
    public interface IValidatable
    {
        Task<bool> ValidateAsync();

        Task ResetAsync();

        Task ResetValidationAsync();
    }
}

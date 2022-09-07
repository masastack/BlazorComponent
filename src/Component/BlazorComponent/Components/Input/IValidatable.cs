namespace BlazorComponent
{
    public interface IValidatable
    {
        Task<bool> ValidateAsync();

        void Reset();

        void ResetValidation();
        
        bool HasError { get; }
    }
}

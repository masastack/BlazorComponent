namespace BlazorComponent
{
    public interface IValidatable
    {
        bool Validate();

        void Reset();

        void ResetValidation();
        
        bool HasError { get; }
    }
}

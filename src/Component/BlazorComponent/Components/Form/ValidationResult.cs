namespace BlazorComponent.Form;

public class ValidationResult
{
    public string? Field { get; set; }

    public string? Message { get; set; }

    public ValidationResultTypes ValidationResultType { get; set; }
}

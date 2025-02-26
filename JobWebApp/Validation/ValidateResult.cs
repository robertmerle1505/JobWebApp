namespace JobWebApp.Validation;

public class ValidateResult
{
    public bool IsValid { get; set; }
    public List<string> ErrorMessages { get; set; } = new();

}

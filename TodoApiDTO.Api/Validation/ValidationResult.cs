namespace TodoApiDTO.Api.Validation
{
    public class ValidationResult
    {
        public ValidationResult(bool isValid, string errorMessage)
        {
            IsValid = isValid;
            ErrorMessage = errorMessage;
        }

        public bool IsValid { get; }

        public string ErrorMessage { get; }
    }
}

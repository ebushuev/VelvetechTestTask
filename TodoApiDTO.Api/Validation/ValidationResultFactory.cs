namespace TodoApiDTO.Api.Validation
{
    public static class ValidationResultFactory
    {
        #region Static

        private static readonly ValidationResult Success = new ValidationResult(true, null);

        public static ValidationResult GetSuccessResult()
        {
            return Success;
        }

        public static ValidationResult GetFailedResult(string message)
        {
            return new ValidationResult(false, message);
        }

        #endregion
    }
}
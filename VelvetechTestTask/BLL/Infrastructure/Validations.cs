namespace BLL.Infrastructure
{
    public static class Validations
    {
        public static void ValidateInput<T>(T input, string paramName)
        {
            if (input is null)
                throw new ValidationException("Argument is null", paramName);
        }
    }
}

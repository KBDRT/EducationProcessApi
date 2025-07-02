namespace Application.Exceptions
{
    public class ValidatorFactoryException : Exception
    {
        public object? ValidatorType;
        public ValidatorFactoryException(string message, object? obj)
            : base(message) 
        { 
            ValidatorType = obj; 
        }
    }
}

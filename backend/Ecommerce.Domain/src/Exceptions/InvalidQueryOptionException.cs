namespace Ecommerce.Domain.src.Exceptions
{
    public class InvalidQueryOptionException : Exception
    {
        public InvalidQueryOptionException()
            : base("The query option provided is invalid.") { }

        public InvalidQueryOptionException(string message)
            : base(message) { }

        public InvalidQueryOptionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
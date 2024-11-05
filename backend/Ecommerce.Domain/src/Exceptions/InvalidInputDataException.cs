
namespace Ecommerce.Domain.src.Exceptions
{
    public class InvalidInputDataException : Exception
    {
        public InvalidInputDataException() : base("The input data provided is invalid.") { }

        public InvalidInputDataException(string message) : base(message) { }

        public InvalidInputDataException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
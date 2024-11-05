namespace Ecommerce.Domain.src.Exceptions
{
    public class UnauthorizedActionException : Exception
    {
        public UnauthorizedActionException() : base("You are not authorized to perform the action") { }

        public UnauthorizedActionException(string message) : base(message) { }

        public UnauthorizedActionException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
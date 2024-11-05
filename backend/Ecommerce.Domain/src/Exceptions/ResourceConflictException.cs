namespace Ecommerce.Domain.src.Exceptions
{
    public class ResourceConflictException : Exception
    {
        public ResourceConflictException() : base("A conflict occurred with the current state of the resource.") { }

        public ResourceConflictException(string message) : base(message) { }

        public ResourceConflictException(string message, Exception innerException)
            : base(message, innerException) { }
    }
}
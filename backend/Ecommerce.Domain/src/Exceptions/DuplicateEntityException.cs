namespace Ecommerce.Domain.src.Exceptions
{
    public class DuplicateEntityException : Exception
    {
        public DuplicateEntityException() : base("The entity already exists.") { }

        public DuplicateEntityException(string message) : base(message) { }
        public DuplicateEntityException(string message, Exception innerException) : base(message, innerException) { }
    }
}

using System.Runtime.Serialization;

namespace Book_Management_API.Data
{
    [Serializable]
    internal class UnauthorizedErrorException : Exception
    {
        public UnauthorizedErrorException()
        {
        }

        public UnauthorizedErrorException(string? message) : base(message)
        {
        }

        public UnauthorizedErrorException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected UnauthorizedErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
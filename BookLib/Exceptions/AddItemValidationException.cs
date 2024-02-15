using System.Runtime.Serialization;

namespace BookLib.Exceptions
{
    [Serializable]
    internal class AddItemValidationException : Exception
    {
        public AddItemValidationException()
        {
        }

        public AddItemValidationException(string? message) : base(message)
        {
        }

        public AddItemValidationException(string? message, Exception? innerException) : base(message, innerException)
        {
        }

        protected AddItemValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
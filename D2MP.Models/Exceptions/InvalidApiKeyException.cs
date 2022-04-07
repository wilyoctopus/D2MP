namespace D2MP.Models.Exceptions
{
    public class InvalidApiKeyException : Exception
    {
        public InvalidApiKeyException()
        {
        }

        public InvalidApiKeyException(string message)
            : base(message)
        {
        }

        public InvalidApiKeyException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}

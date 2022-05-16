namespace PinService.Domain.Core.Seed
{
    public abstract class BaseException : Exception
    {
        public ErrorCode ErrorCode { get; }

        public BaseException(ErrorCode errorCode, string message) : base(message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
            }

            ErrorCode = errorCode;
        }

        public BaseException(ErrorCode errorCode, string message, Exception? innerException) : base(message, innerException)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentException($"'{nameof(message)}' cannot be null or whitespace.", nameof(message));
            }

            ErrorCode = errorCode;
        }
    }
}

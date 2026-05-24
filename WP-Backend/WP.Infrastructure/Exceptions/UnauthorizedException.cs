namespace WP.Infrastructure.Exceptions
{
    public class UnauthorizedException : ApplicationException
    {
        public readonly string StatusCode = string.Empty;

        public UnauthorizedException(string statusCode, string message)
            : base(message)
        {
            StatusCode = statusCode;
        }

        public UnauthorizedException(string statusCode, string message, Exception innerException)
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}

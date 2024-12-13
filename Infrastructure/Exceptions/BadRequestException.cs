namespace Infrastructure.Exceptions;

public class BadRequestException : Exception
{
    public BadRequestException()
        : base("Bad Request.")
    {
    }

    public BadRequestException(string message)
        : base(message)
    {
    }
}
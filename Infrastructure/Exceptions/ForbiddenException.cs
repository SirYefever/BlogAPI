namespace Infrastructure.Exceptions;

public class ForbiddenException: Exception
{
    
    // Default constructor
    public ForbiddenException() 
        : base("Access denied.")
    {
    }

    // Constructor that accepts a custom message
    public ForbiddenException(string message) 
        : base(message)
    {
    }

}
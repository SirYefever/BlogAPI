using Application.Auth;
using Infrastructure.Exceptions;

namespace Infrastructure;

public class PasswordHasher : IPasswordHasher
{
    public string Generate(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool Verify(string password, string hashedPassword)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(
            password,
            hashedPassword
        );
    }

    public void ConfirmLogging(string password, string hashedPassword)
    {
        if (!Verify(password, hashedPassword))
            throw new BadRequestException("Failed to login.");
    }
}
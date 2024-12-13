using System.Security.Cryptography;
using BCrypt.Net;
using Application.Auth;
using Infrastructure.Exceptions;
using Microsoft.IdentityModel.Tokens;

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
            text: password,
            hash: hashedPassword
            );
    }

    public void ConfirmLogging(string password, string hashedPassword)
    {
        if (!Verify(password, hashedPassword))
            throw new BadRequestException("Failed to login.");
    }
}
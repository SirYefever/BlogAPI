namespace Application;

public interface ITokenGenerator
{
    string GenerateToken(string email);
}
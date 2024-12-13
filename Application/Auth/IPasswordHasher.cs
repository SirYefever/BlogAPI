namespace Application.Auth;

public interface IPasswordHasher
{
    string Generate(string password);
    bool Verify(string password, string hashedPassword);
    void ConfirmLogging(string password, string hashedPassword);
}
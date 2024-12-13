using Core.Models;

namespace Core.ServiceContracts;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}
using Application.Auth;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class UserService : IUserService
{
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher,
        IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }

    public async Task<User> GetUserById(Guid userId)
    {
        return await _userRepository.GetById(userId);
    }

    public async Task<User> CreateUser(User user)
    {
        var token = _jwtProvider.GenerateToken(user);
        user.Token = token;
        user.CreateTime = DateTime.UtcNow;
        await _userRepository.Add(user);

        return user;
    }

    public async Task UpdateUser(Guid userToBeUpdatedId, User updatedUser)
    {
        await _userRepository.Update(userToBeUpdatedId, updatedUser);
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmail(email);

        _passwordHasher.ConfirmLogging(password, user.HashedPassword);

        var token = _jwtProvider.GenerateToken(user);
        var newUserData = user;
        newUserData.Token = token;
        await _userRepository.Update(user.Id, newUserData);

        return token;
    }

    public async Task Logout(Guid userId)
    {
        await _userRepository.Logout(userId);
    }
}
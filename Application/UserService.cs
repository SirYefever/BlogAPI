using System.Runtime.CompilerServices;
using Application.Auth;
using Application.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UserService(IUserRepository userRepository, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _userRepository = userRepository;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    // Service is a middle layer between controller and repository. it's supposed to manage console logging,
    // validation(that's not related to DB(whatever that means)) TODO: delete this comment


    public async Task<User> GetUserById(Guid userId)
    {
        var user = await _userRepository.GetById(userId);
        return user;
    }

    public async Task<User> CreateUser(User user)// Todo: figure out weather this needs to return user or token
    {
        // Console logging is supposed to be here
        var token = _jwtProvider.GenerateToken(user);
        user.Token = token;
        user.CreateTime = DateTime.UtcNow;
        //TODO: figure out how to handle exception form userRepository
        try
        {
            await _userRepository.GetByEmail(user.Email);
        }
        catch(NullReferenceException)//TODO: figure out how to handle this case (maybe change the way UserRepository behaves)
        {
            await _userRepository.Add(user);
        }

        return user;
    }

    public async Task UpdateUser(Guid userToBeUpdatedId, User updatedUser)
    {
        await _userRepository.Update(userToBeUpdatedId, updatedUser);
    }

    public Task<User> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }
    //TODO: Is there supposed to be async here?
    public async Task<string> Login(string email, string password)
    {//TODO: figure out weather this is the place where all this logic is supposed to be in.
        var user = await _userRepository.GetByEmail(email);
        var authenticationAllowed = _passwordHasher.Verify(password, user.HashedPassword);
        if (!authenticationAllowed)
        {
            throw new Exception("Failed to login");//TODO: figure out how to handle this case properly
        }

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
using System.Runtime.CompilerServices;
using Application.Auth;
using Application.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;
    private readonly ITokenGenerator _tokenGenerator;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtProvider _jwtProvider;

    public UserService(IUserStore userStore, ITokenGenerator tokenGenerator, IPasswordHasher passwordHasher, IJwtProvider jwtProvider)
    {
        _userStore = userStore;
        _tokenGenerator = tokenGenerator;
        _passwordHasher = passwordHasher;
        _jwtProvider = jwtProvider;
    }
    // Service is a middle layer between controller and repository. it's supposed to manage console logging,
    // validation(that's not related to DB(whatever that means)) TODO: delete this comment


    public Task<User> GetUserById(Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<User> CreateUser(User user)// Todo: figure out weather this needs to return user or token
    {
        // Console logging is supposed to be here
        var token = _tokenGenerator.GenerateToken(user.Email);
        user.Token = token;
        //TODO: figure out how to handle exception form userRepository
        try
        {
            await _userStore.GetByEmail(user.Email);
        }
        catch(NullReferenceException)//TODO: figure out how to handle this case (maybe change the way UserRepository behaves)
        {
            await _userStore.Add(user);
        }

        return user;
    }

    public Task<User> UpdateUser(User userToBeUpdated, User updatedUser)
    {
        throw new NotImplementedException();
    }

    public Task<User> DeleteUser(User user)
    {
        throw new NotImplementedException();
    }
    //TODO: Is there supposed to be async here?
    public async Task<string> Login(string email, string password)
    {
        var user = await _userStore.GetByEmail(email);
        var authenticationAllowed = _passwordHasher.Verify(password, user.HashedPassword);
        if (!authenticationAllowed)
        {
            throw new Exception("Failed to login");//TODO: figure out how to handle this case properly
        }

        var token = _jwtProvider.GenerateToken(user);
        return token;
    }
}
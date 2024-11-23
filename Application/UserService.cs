using Application.Dto;
using Core.InterfaceContracts;
using Core.Models;
using Core.ServiceContracts;

namespace Application;

public class UserService : IUserService
{
    private readonly IUserStore _userStore;
    private readonly ITokenGenerator _tokenGenerator;

    public UserService(IUserStore userStore, ITokenGenerator tokenGenerator)
    {
        _userStore = userStore;
        _tokenGenerator = tokenGenerator;
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
        _userStore.Add(user);
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

    public User Login(string email, string password)
    {
        var user = _userStore.GetByCreds(email, password);
        return user;
    }
}
using API.Dto;
using Application.Auth;
using Core.Models;
using Infrastructure;

namespace API.Converters;

public class UserConverters
{
    private readonly IPasswordHasher _passwordHasher;

    public UserConverters(IPasswordHasher passwordHasher)
    {
        _passwordHasher = passwordHasher;
    }

    public User UserDtoToUser(UserDto dto)
    {
        //TODO: Dedicate what to do with password property
        var user = new User();
        user.FullName = dto.FullName;
        user.Email = dto.Email;
        user.BirthDate = dto.BirthDate;
        user.Gender = dto.Gender;
        user.PhoneNumber = dto.PhoneNumber;
        return user;
    }

    public User UserRegisterModelToUser(UserRegisterModel model)
    {
        var user = new User();
        var hashedPassword = _passwordHasher.Generate(model.Password);
        user.FullName = model.FullName;
        user.Email = model.Email;
        user.HashedPassword = hashedPassword;
        user.BirthDate = model.BirthDate;
        user.Gender = model.Gender;
        user.PhoneNumber = model.PhoneNumber;
        return user;
    }

    public static UserDto UserToUserDto(User user)
    {
        var userDto = new UserDto();
        userDto.Id = user.Id;
        userDto.FullName = user.FullName;
        userDto.Email = user.Email;
        userDto.BirthDate = user.BirthDate;
        userDto.Gender = user.Gender;
        userDto.PhoneNumber = user.PhoneNumber;
        return userDto;
    }

    public UserDto UserRegisterModelToUserDto(UserRegisterModel model)
    {
        var userDto = new UserDto();
        userDto.FullName = model.FullName;
        userDto.Email = model.Email;
        userDto.BirthDate = model.BirthDate;
        userDto.Gender = model.Gender;
        userDto.PhoneNumber = model.PhoneNumber;
        return userDto;
    }


    public UserRegisterModel UserDtoToUserRegisterModelDto(UserDto dto)
    {
        //TODO: Dedicate what to do with password property
        var model = new UserRegisterModel();
        model.FullName = dto.FullName;
        model.Email = dto.Email;
        model.BirthDate = dto.BirthDate;
        model.Gender = dto.Gender;
        model.PhoneNumber = dto.PhoneNumber;
        return model;
    }
    
}
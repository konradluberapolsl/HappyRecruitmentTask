using System.Linq.Expressions;
using TeslaRent.Application.Users.Models;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Users.Abstraction;

public interface IUserService
{
    Task<UserDto> CreateUser(CreateUserRequest createUserRequest);
    Task<UserDto> GetUser(Expression<Func<Domain.Entities.User, bool>> predicate);
    Task<UserDto?> GetUserByEmail(string email);
}
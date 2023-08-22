using System.Linq.Expressions;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Application.Users.Abstraction;
using TeslaRent.Application.Users.Models;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Users;

public class UserService : IUserService
{
    private readonly IDbContext _dbContext;
    private readonly IMapper _mapper;

    public UserService(IDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<UserDto> CreateUser(CreateUserRequest createUserRequest)
    {
        var user = _mapper.Map<User>(createUserRequest);

        await _dbContext.Users.AddAsync(user);

        await _dbContext.SaveChangesAsync(CancellationToken.None);

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetUser(Expression<Func<User, bool>> predicate)
    {
        var user = await _dbContext.Users.SingleOrDefaultAsync(predicate);
        
        if (user == null)
        {
            throw new Exception("User not found");
        }

        return _mapper.Map<UserDto>(user);
    }
}
using Microsoft.AspNetCore.Mvc;
using TeslaRent.Application.Common.Abstraction;
using TeslaRent.Application.Users.Abstraction;
using TeslaRent.Application.Users.Models;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;

    public UsersController(IUserService userService, ICurrentUserService currentUserService)
    {
        _userService = userService;
        _currentUserService = currentUserService;
    }

    [HttpPost]
    public async Task<UserDto> CreateUser(CreateUserRequest request)
    {
        return await _userService.CreateUser(request);
    }

    [HttpGet("{id}")]
    public async Task<UserDto> GetUserById(int id)
    {
        return await _userService.GetUser(u => u.Id == id);
    }

    [HttpGet("loggedUser")]
    public async Task<UserDto> GetLoggedInUser()
    {
        return await _userService.GetUser(u => u.Id == _currentUserService.UserId);
    }
}
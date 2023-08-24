using TeslaRent.Application.Common.AutoMapper;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Users.Models;

public class CreateUserRequest : IMapFrom<User>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
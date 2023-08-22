using TeslaRent.Application.Common.AutoMapper;
using TeslaRent.Domain.Entities;

namespace TeslaRent.Application.Users.Models;

public class UserDto : IMapFrom<User>
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
}
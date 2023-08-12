using Microsoft.AspNetCore.Mvc;

namespace TeslaRent.API.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
    [HttpGet("ping")]
    public string Ping() => "pong";
}
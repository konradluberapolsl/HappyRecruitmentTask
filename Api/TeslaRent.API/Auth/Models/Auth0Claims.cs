namespace TeslaRent.API.Auth.Models;

public class Auth0Claims
{
    private const string Namespace = "https://teslarent.com/claims";
    public static string Email => $"{Namespace}/email";
}
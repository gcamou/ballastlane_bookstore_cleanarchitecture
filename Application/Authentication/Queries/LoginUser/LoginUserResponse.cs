namespace Application.Authentication.Queries.LoginUser;

public class LoginUserResponse
{
    public required string username { get; set; }
    public required string token { get; set; }
}
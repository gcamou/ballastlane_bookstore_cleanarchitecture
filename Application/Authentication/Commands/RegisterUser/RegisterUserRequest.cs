namespace Application.Authentication.Commands.RegisterUser;
public class RegisterUserRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
}
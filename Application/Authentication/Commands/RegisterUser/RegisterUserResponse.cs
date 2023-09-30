namespace Application.Authentication.Commands.RegisterUser;

public class RegisterUserResponse
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Role { get; set; }
    public required string Token { get; set; }
}
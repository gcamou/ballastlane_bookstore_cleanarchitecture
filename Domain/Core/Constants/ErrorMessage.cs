namespace Domain.Core.Constants;
public class ErrorMessage
{
    public const string GenericExceptionMessage = "The system ran into a problem, if the error persists contact support";
    public const string BookNotFound = "The Book was not found";
    public const string CreateBookError = "The book can't be create it.";
    public const string CreateBookTitleExistError = "The book already exist.";
    public const string UpdateBookError = "The book can't be update it.";
    public const string DeleteBookError = "The book can't be delete it.";
    public const string RegisterUserError = "The user can't be register it.";
    public const string LoginUserNotFound = "The user {0} does not exist register.";
    public const string PasswordInValid = "The password is invalid.";
    public const string RoleNotFound = "The role {0} does not found.";
    public const string TokenGenerationError = "Token generation failed.";
    public const string BadRequest = "Invalid request payload.";
    public const string ServerError = "An unexpected error occurred.";
}
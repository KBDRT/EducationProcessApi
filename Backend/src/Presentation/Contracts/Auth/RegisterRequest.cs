namespace Presentation.Contracts.Auth
{
    public record RegisterRequest
    (
        string Login,
        string Password
    );
}

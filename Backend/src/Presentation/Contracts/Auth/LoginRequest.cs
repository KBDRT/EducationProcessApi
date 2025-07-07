namespace Presentation.Contracts.Auth
{
    public record LoginRequest
    (
        string Login,
        string Password
    );
}

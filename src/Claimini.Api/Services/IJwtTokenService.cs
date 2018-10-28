namespace Claimini.Api.Services
{
    public interface IJwtTokenService
    {
        string BuildToken(string email);
    }
}

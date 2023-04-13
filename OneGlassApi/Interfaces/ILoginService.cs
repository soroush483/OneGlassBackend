using OneGlassApi.Models;

namespace OneGlassApi.Interfaces
{
    public interface ILoginService
    {
        ApiCredentials GenearteJwtToken(string username, string password);
    }
}

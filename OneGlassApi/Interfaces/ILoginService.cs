namespace OneGlassApi.Interfaces
{
    public interface ILoginService
    {
        string GenearteJwtToken(string username, string password);
    }
}

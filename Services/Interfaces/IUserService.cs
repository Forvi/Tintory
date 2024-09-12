namespace COLOR.Services.Interfaces;

public interface IUserService
{
    public Task Register(string name, string email, string password, CancellationToken ct);
    public Task<string> Login(string email, string password, CancellationToken ct);

}
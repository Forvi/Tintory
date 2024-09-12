using COLOR.Domain.Entities;

namespace COLOR.Data.Repositories.Interfaces;

public interface IUserRepository
{
    public Task AddUser(UserEntity user, CancellationToken ct);
    public Task<UserEntity?> GetByEmail(string email, CancellationToken ct);
}
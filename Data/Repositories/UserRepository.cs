using COLOR.Data.Repositories.Interfaces;
using COLOR.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace COLOR.Data.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(AppDbContext dbContext, ILogger<UserRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task AddUser(UserEntity user, CancellationToken ct)
    {
        await _dbContext.AddAsync(user, ct);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<UserEntity?> GetByEmail(string email, CancellationToken ct)
    {
        try
        {
            var user = await _dbContext.UserEntities
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Email == email, ct);

            return user;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Email not found");
            throw;
        }
    }
}
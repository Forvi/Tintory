using COLOR.Domain.Entities;

namespace COLOR.Services.Interfaces;

public interface IJwtProvider
{
    public string GenerateToken(UserEntity user);
}
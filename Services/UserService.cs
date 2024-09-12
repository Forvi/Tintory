using System.ComponentModel.Design;
using COLOR.Data.Repositories.Interfaces;
using COLOR.Domain.Entities;
using COLOR.Services.Interfaces;

namespace COLOR.Services;

public class UserService : IUserService
{
    private readonly IPasswordHasher _hasher;
    private readonly IUserRepository _userRepository;
    private readonly IJwtProvider _jwtProvider;
    private readonly ILogger<UserService> _logger;

    public UserService(IUserRepository userRepository, IPasswordHasher hasher, IJwtProvider jwtProvider, ILogger<UserService> logger)
    {
        _userRepository = userRepository;
        _hasher = hasher;
        _jwtProvider = jwtProvider;
        _logger = logger;
    }
    
    public async Task Register(string name, string email, string password, CancellationToken ct)
    {
        var hashedPassword = _hasher.Generate(password);
        var user = UserEntity.Create(Guid.NewGuid(), name, hashedPassword, email);
        await _userRepository.AddUser(user, ct);
    }

    public async Task<string> Login(string email, string password, CancellationToken ct)
    {
        try
        {
            var user = await _userRepository.GetByEmail(email, ct);
            if (user == null)
                throw new UnauthorizedAccessException("Invalid email"); 
        
            var isPasswordValid = _hasher.Verify(password, user.PasswordHash);
            if (!isPasswordValid)
                throw new UnauthorizedAccessException("Invalid password"); 
        
            var token = _jwtProvider.GenerateToken(user);
    
            return token;
        }
        catch (Exception e)
        {
            _logger.LogError(e, "login error");
            throw;
        }
    }
}
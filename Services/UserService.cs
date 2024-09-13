using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using COLOR.Data.Repositories.Interfaces;
using COLOR.Data.Validation;
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
        try
        {
            var validate = new UserValidation();
            
            var isValidPassword = validate.ValidatePassword(password);
            if (isValidPassword == false)
                throw new ValidationException("Password is too short");
                
            var hashedPassword = _hasher.Generate(password);
            var user = UserEntity.Create(Guid.NewGuid(), name, hashedPassword, email);
        
            var validateResult = await validate.ValidateAsync(user, ct);
            if (!validateResult.IsValid)
            {
                foreach (var error in validateResult.Errors)
                    _logger.LogError($"Property: {error.PropertyName}; Error: {error.ErrorMessage}");

                throw new ValidationException("Validation passed");
            }
        
            await _userRepository.AddUser(user, ct);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Register error");
            throw;
        }
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
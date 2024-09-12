using COLOR.DTOs;
using COLOR.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace COLOR.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly ILogger<UserController> _logger;
    private readonly IUserService _userService;

    public UserController(IUserService userService, ILogger<UserController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserDto request, CancellationToken ct)
    {
        try
        {
            await _userService.Register(request.Name, request.Email, request.Password, ct);
            return Ok(200);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Register error");
            throw;
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserDto request, CancellationToken ct)
    {
        var context = HttpContext.Response.Cookies;
        var token = await _userService.Login(request.Email, request.Password, ct);
        context.Append("fjIgWa", token);
        
        return Ok();
    }
}